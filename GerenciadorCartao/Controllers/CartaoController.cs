using GerenciadorCartao.Context;
using GerenciadorCartao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GerenciadorCartao.Controllers
{
    public class CartaoController : Controller
    {
        private readonly GerenciadorContext _context;

        public CartaoController(GerenciadorContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult NovoCartao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoCartao(Cartao cartao)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(cartao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListagemCartoes));
            }

            return View(cartao);
        }

        public async Task<IActionResult> ListagemCartoes()
        {
            return View(await _context.Cartoes.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarCartao(int cartaoId)
        {
            Cartao cartao = await _context.Cartoes.FindAsync(cartaoId);

            if (cartao == null)
            {
                return NotFound();
            }

            return View(cartao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarCartao(int cartaoId, Cartao cartao)
        {
            if (cartaoId != cartao.CartaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(cartao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListagemCartoes));
            }

            return View(cartao);
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirCartao(int cartaoId)
        {
            Cartao cartao = await _context.Cartoes.FindAsync(cartaoId);
            _context.Cartoes.Remove(cartao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListagemCartoes));
        }
    }
}
