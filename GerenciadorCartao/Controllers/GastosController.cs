using GerenciadorCartao.Context;
using GerenciadorCartao.Models;
using GerenciadorCartao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCartao.Controllers
{
    public class GastosController : Controller
    {
        private readonly GerenciadorContext _context;

        public GastosController(GerenciadorContext contexto)
        {
            _context = contexto;
        }

        public async Task<IActionResult> ListagemGasto(int cartaoId)
        {
            Cartao cartao = await _context.Cartoes.FindAsync(cartaoId);
            double soma = await _context.Gastos.Where(c => c.CartaoId == cartaoId).SumAsync(c => c.Valor);

            GastosViewModel gastosViewModel = new GastosViewModel
            {
                CartaoId = cartaoId,
                NumeroCartao = cartao.NumeroCartao,
                ListaGastos = await _context.Gastos.Where(c => c.CartaoId == cartaoId).ToListAsync(),
                PorcentagemGasta = Convert.ToInt32((soma / cartao.Limite) * 100)
            };

            return View(gastosViewModel);
        }

        [HttpGet]
        public IActionResult NovoGasto(int cartaoId)
        {
            Gasto gasto = new Gasto
            {
                CartaoId = cartaoId
            };

            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoGasto(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListagemGasto), new { cartaoId = gasto.CartaoId });
            }

            return View(gasto);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarGasto(int gastoId)
        {
            Gasto gasto = await _context.Gastos.FindAsync(gastoId);

            if (gasto == null)
                return NotFound();

            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarGasto(int gastoId, Gasto gasto)
        {
            if (gastoId != gasto.GastoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListagemGasto), new { cartaoId = gasto.CartaoId });
            }

            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirGasto(int gastoId)
        {
            Gasto gasto = await _context.Gastos.FindAsync(gastoId);
            _context.Remove(gasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListagemGasto), new { cartaoId = gasto.CartaoId });
        }

    }
}
