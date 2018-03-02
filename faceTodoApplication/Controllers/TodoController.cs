using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using faceTodoApplication.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Options;


namespace faceTodoApplication.Controllers
{

    public class TodoController : Controller
    {
        private readonly TodoesContext _context;
        private readonly IOptions<AzureStorageConfig> _appOptions;
        private readonly IOptions<FaceApiConfig> _faceApiOptions;
        public TodoController(TodoesContext context, IOptions<AzureStorageConfig> appOptions, IOptions<FaceApiConfig> faceApiOptions)
        {
            _context = context;
            _appOptions = appOptions;
            _faceApiOptions = faceApiOptions;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Todo.ToListAsync());
        }

        // GET: Todo/loginForm
        public IActionResult loginForm()
        {
            ViewBag.OperationMessage = TempData["OperationMessage"];
            return View();
        }

        // GET: Todo/imgUploadForm
        public IActionResult imgUploadForm()
        {
            return View();
        }

        // GET: Todo/mainForm
        public IActionResult mainForm()
        {
            return View();
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .SingleOrDefaultAsync(m => m.ID == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Mail,Password,GoogleToken,TodoTitle,PersonId")] Todo todo)
        {

            if (ModelState.IsValid)
            {
                if (TodoExistsMail(todo.Mail) == false)
                {
                    _context.Add(todo);
                    await _context.SaveChangesAsync();

                    int userID = todo.ID;
                    if (todo.PersonId != null)
                    {
                        ViewBag.imgRegister = "画像のアップが完了しています。OK";
                    }
                    else ViewBag.imgNoRegister = "画像のアップが未完了です!";
                    ViewBag.uName = "ログイン中:" + todo.Name;
                    TempData["userID"] = userID;
                    return View("mainForm");

                }
                else
                {
                    ViewBag.usedMsg = "このメールアドレスは既に使用されています。";
                    return View("Create");
                }
            }
            return View(todo);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.ID == id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Mail,Password,GoogleToken,TodoTitle,PersonId")] Todo todo)
        {
            if (id != todo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var key = _appOptions.Value.AccountKey;
                    var name = _appOptions.Value.AccountName;
                    imgUpload img = new imgUpload();
                    string imgPath = img.ImgUpload(Request.HttpContext, name, key);
                    _context.Update(todo);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .SingleOrDefaultAsync(m => m.ID == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.ID == id);
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.ID == id);
        }

        private bool TodoExistsMail(string mail)
        {
            return _context.Todo.Any(e => e.Mail.Contains(mail));
        }

        // POST: Todo/loginForm
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginForm(string email, string password)
        {

            if (email == null || password == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.Password.Contains(password)
                                                                && m.Mail.Contains(email));
            if (todo == null)
            {
                String message = String.Format("ログインに失敗しました。メールアドレスまたはパスワードが間違っています。", "");
                TempData["OperationMessage"] = message;
                return RedirectToAction(nameof(loginForm));
            }
            else
            {
                int userID = todo.ID;
                if (todo.PersonId != null)
                {
                    ViewBag.imgRegister = "画像のアップが完了しています。OK";
                }
                else ViewBag.imgNoRegister = "画像のアップが未完了です!";
                ViewBag.name = "ログイン中:" + todo.Name;
                TempData["userID"] = userID;
                return View("mainForm");
            }

        }

        // POST: Todo/mainForm
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MainForm()
        {
            if (ModelState.IsValid)
            {
                int userID = int.Parse(TempData["userID"].ToString());
                var todo = await _context.Todo.SingleOrDefaultAsync(m => m.ID == userID);

                try
                {
                    //顔画像のパス取得
                    var key = _appOptions.Value.AccountKey;
                    var name = _appOptions.Value.AccountName;
                    imgUpload img = new imgUpload();
                    string imgPath = img.ImgUpload(Request.HttpContext, name, key);

                    //名前でPERSON作成＋画像をPERSONへ追加
                    var subscriptionKey = _faceApiOptions.Value.Subscriptionkey;
                    var groupName = _faceApiOptions.Value.GroupName;
                    AddPerson face = new AddPerson();
                    string personId = face.addPerson(subscriptionKey, groupName, todo.Name, imgPath);
                    todo.PersonId = personId;
                    _context.Update(todo);
                    await _context.SaveChangesAsync();

                    if (todo.PersonId != null)
                    {
                        ViewBag.imgRegister = "画像のアップが完了しています。OK";
                    }
                    else ViewBag.imgNoRegister = "画像のアップが未完了です!";
                    ViewBag.name = "ログイン中:" + todo.Name;
                    TempData["userID"] = userID;
                    return View("mainForm");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View();
        }
    }
}
