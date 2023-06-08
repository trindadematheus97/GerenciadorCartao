using GerenciadorCartao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCartao.Context
{
    public class GerenciadorContext : DbContext
    {
        public DbSet<Cartao> Cartoes { get; set; }

        public DbSet<Gasto> Gastos { get; set; }

        public GerenciadorContext(DbContextOptions<GerenciadorContext> opcoes) : base(opcoes)
        {

        }
    }
}
