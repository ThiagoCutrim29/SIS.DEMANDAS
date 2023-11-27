using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SIGD.Filters;
using SIGD.Models;
using SIGD.seção;

namespace SIGD.Controllers
{
    [PaginaUsuarioLogado]
    public class DemandasController : Controller
    {
        private readonly Contexto _context ;
        

        public DemandasController(Contexto context )
        {
            _context = context;
       
        }

        //ARMAZENANDO A VAIRAVEL DE CLASSE PARA SALVAR O NOME DO USUARIO DA SEÇÃO
        //para usar em deirentes metodos

        private string GetNomeUsuarioSessao()
        {
            string usuarioJson = HttpContext.Session.GetString("SecaoUsuarioLogado");

            if (!string.IsNullOrEmpty(usuarioJson))
            {
                // Use System.Text.Json to deserialize the JSON
                var usuario = JsonSerializer.Deserialize<Usuario>(usuarioJson);

                return usuario.Nome;
            }

            return null;
        }

        public string TextoBotao { get; set; }

        // GET: Demandas

        //chamei a variavel de calsse e implementei na função pra chamar a lista so com as demandas no qual o usuario é demandante
        public async Task<IActionResult> IndexAdm()
        {
            var nomeUsuarioSessao = GetNomeUsuarioSessao();

            if (string.IsNullOrEmpty(nomeUsuarioSessao))
            {
                return Problem("Nome de usuário da sessão não encontrado.");
            }

            var demandas = await _context.Demandas
                .Where(m => m.Demandante == nomeUsuarioSessao)
                .ToListAsync();

            return View(demandas);
        }
        public async Task<IActionResult> Index()
        {
            var demandas = await _context.Demandas
            .Where(m => m.status == "EM ANALISE" || m.status == "CRIADO")
            .ToListAsync();

            return demandas != null ?
                View(demandas) :
                Problem("Lista de demandas é nula.");
        }


        // GET: Demandas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Demandas == null)
            {
                return NotFound();
            }

            var demanda = await _context.Demandas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demanda == null)
            {
                return NotFound();
            }

            return View(demanda);
        }
        public async Task<IActionResult> DetailsProgramador(int? id)
        {
            if (id == null || _context.Demandas == null)
            {
                return NotFound();
            }

            var demanda = await _context.Demandas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demanda == null)
            {
                return NotFound();
            }

      

            return View(demanda);
        }



        // GET: Demandas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Demandas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,titulo,descricao,status,DtCadastro,Prazo,Demandante,Programador,Observacao")] Demanda demanda)
        {
            if (ModelState.IsValid)
            {
                demanda.DtCadastro = DateTime.Now;
                demanda.status = "CRIADO";

                string nomeUsuarioSessao = GetNomeUsuarioSessao();

                if (!string.IsNullOrEmpty(nomeUsuarioSessao))
                {
                    demanda.Demandante = nomeUsuarioSessao;
                }
                else
                {
                    // Handle the case where the user's data is not found in the session.
                    // You can log an error, show a message, or take other appropriate action.
                }

                _context.Add(demanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(demanda);
        }


        // GET: Demandas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null || _context.Demandas == null)
            {
                return NotFound();
            }

            var demanda = await _context.Demandas.FindAsync(id);
            if (demanda == null)
            {
                return NotFound();
            }
       
            return View(demanda);
        }

        // POST: Demandas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,titulo,descricao,status,DtCadastro,Prazo,Demandante,Programador,Observacao")] Demanda demanda)
        {
            demanda.DtCadastro = DateTime.Now;
            demanda.status = "CRIADO";
        
            if (id != demanda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandaExists(demanda.Id))
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
            
            return View(demanda);
        }

        // GET: Demandas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Demandas == null)
            {
                return NotFound();
            }

            var demanda = await _context.Demandas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demanda == null)
            {
                return NotFound();
            }

            return View(demanda);
        }

        // POST: Demandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Demandas == null)
            {
                return Problem("Entity set 'Contexto.Demandas'  is null.");
            }
            var demanda = await _context.Demandas.FindAsync(id);
            if (demanda != null)
            {
                _context.Demandas.Remove(demanda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemandaExists(int id)
        {
          return (_context.Demandas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        ////listar todas as demandas de todos os usuarios que estao criado
        public async Task<IActionResult> DemandasPendentes()
        {
            var demandas = await _context.Demandas
                .Where(m => m.status == "CRIADO")
                .ToListAsync();

            return demandas != null ?
                View(demandas) :
                Problem("Lista de demandas é nula.");
        }
        ////listar todas as demandas de todos os usuarios que estao em analise
        public async Task<IActionResult> DemandasAnalise()
        {
            var nomeUsuarioSessao = GetNomeUsuarioSessao();

            if (string.IsNullOrEmpty(nomeUsuarioSessao))
            {
                return Problem("Nome de usuário da sessão não encontrado.");
            }


            var demandas = await _context.Demandas
                .Where(m => m.status == "EM ANALISE")
                .Where(m => m.Programador == nomeUsuarioSessao)
                .ToListAsync();

            return demandas != null ?
                View(demandas) :
                Problem("Lista de demandas é nula.");
        } 
        //listar todas as demandas de todos os usuarios que estao criadas ou em analise
      

        //DemandasFinalizadas

        public async Task<IActionResult> DemandasFinalizadas()
        {
            var nomeUsuarioSessao = GetNomeUsuarioSessao();

            if (string.IsNullOrEmpty(nomeUsuarioSessao))
            {
                return Problem("Nome de usuário da sessão não encontrado.");
            }

            var demandas = await _context.Demandas
                .Where(m => m.Programador == nomeUsuarioSessao)
                .Where(m => m.status == "Finalizado")

                .ToListAsync();

            return View(demandas);
        }

        
        // POST: Demandas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> modalAceitarDemanda(int id, [Bind("Id,titulo,descricao,status,DtCadastro,Prazo,Demandante,Programador,Observacao")] Demanda novaDemanda)
        {
            // Carregar a demanda existente do banco de dados
            Demanda demandaExistente = await _context.Demandas.FindAsync(id);

            if (demandaExistente == null)
            {
                return NotFound();
            }

            // Atualizar apenas os campos necessários
            demandaExistente.DtCadastro = DateTime.Now;
            demandaExistente.status = "EM ANALISE";
            demandaExistente.Prazo = novaDemanda.Prazo; // Atualize o campo Prazo

            string nomeUsuarioSessao = GetNomeUsuarioSessao();

            if (!string.IsNullOrEmpty(nomeUsuarioSessao))
            {
                demandaExistente.Programador = nomeUsuarioSessao;
            }
            else
            {
                // Lide com o caso em que os dados do usuário não foram encontrados na sessão.
                // Você pode registrar um erro, mostrar uma mensagem ou tomar outras ações apropriadas.
            }

            // Atualizar a demanda no banco de dados
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demandaExistente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DemandasPendentes));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandaExists(demandaExistente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }



            // Se o modelo não for válido, retorne a visão com a demanda existente
            return View(demandaExistente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> modalFinalizarDemanda(int id, [Bind("Id,titulo,descricao,status,DtCadastro,Prazo,Demandante,Programador,Observacao")] Demanda novaDemanda)
        {
            // Carregar a demanda existente do banco de dados
            Demanda demandaExistente = await _context.Demandas.FindAsync(id);

            if (demandaExistente == null)
            {
                return NotFound();
            }

            // Atualizar apenas os campos necessários
            demandaExistente.DtCadastro = DateTime.Now;
            demandaExistente.status = "FINALIZADO";
            demandaExistente.Observacao = novaDemanda.Observacao;

            string nomeUsuarioSessao = GetNomeUsuarioSessao();

            if (!string.IsNullOrEmpty(nomeUsuarioSessao))
            {
                demandaExistente.Programador = nomeUsuarioSessao;
            }
            else
            {
                // Lide com o caso em que os dados do usuário não foram encontrados na sessão.
                // Você pode registrar um erro, mostrar uma mensagem ou tomar outras ações apropriadas.
            }

            // Atualizar a demanda no banco de dados
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demandaExistente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DemandasAnalise));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandaExists(demandaExistente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }



            // Se o modelo não for válido, retorne a visão com a demanda existente
            return View("Index");
        }

        

    }
}
