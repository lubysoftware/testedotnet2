using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.WebApp.ViewModels;
using Desafio.Business.Interfaces;
using AutoMapper;
using Desafio.Business.Models;
using Microsoft.AspNetCore.Authorization;

namespace Desafio.WebApp.Controllers
{
    //[Authorize]
    //public class JogoController : Controller
    //{
    //    private readonly IJogoService _jogoService;
    //    private readonly IEmprestimoService _emprestimoService;
    //    private readonly IJogoRepository _jogoRepository;
    //    private readonly IMapper _mapper;

    //    public JogoController(IJogoService jogoService, IEmprestimoService emprestimoService, IJogoRepository jogoRepository, IMapper mapper)
    //    {
         
    //        _jogoService = jogoService;
    //        _emprestimoService = emprestimoService;
    //        _jogoRepository = jogoRepository;
    //        _mapper = mapper;
    //    }


    //    public async Task<IActionResult> Index()
    //    {
    //        return View(_mapper.Map<IEnumerable<JogoViewModel>>(await _jogoService.BuscarTodos()));
    //    }


    //    public async Task<IActionResult> Details(int id)
    //    {
    //        var jogoViewModel = _mapper.Map<JogoViewModel>(await _jogoService.BuscarPorID(id));

    //        jogoViewModel.Emprestimo = _mapper.Map<IEnumerable<EmprestimoViewModel>>(await _emprestimoService.ObterEmprestimosJogo(id));

    //        return View(jogoViewModel);
    //    }


    //    public IActionResult Create()
    //    {
    //        return View();
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Create(JogoViewModel jogoViewModel)
    //    {
    //        if (!ModelState.IsValid) return View(jogoViewModel);

    //        var pessoa = _mapper.Map<Jogo>(jogoViewModel);
    //        await _jogoService.Adicionar(pessoa);

    //        return RedirectToAction("Index");
    //    }


    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var jogoViewModel = _mapper.Map<JogoViewModel>(await _jogoService.BuscarPorID(id));

    //        return View(jogoViewModel);
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Edit(JogoViewModel jogoViewModel)
    //    {
    //        if (!ModelState.IsValid) return View(jogoViewModel);


    //        var jogo = _mapper.Map<Jogo>(jogoViewModel);
    //        await _jogoService.Atualizar(jogo);

    //        return RedirectToAction("Index");
    //    }



    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var jogoViewModel = _mapper.Map<JogoViewModel>(await _jogoService.BuscarPorID(id));

    //        return View(jogoViewModel);
    //    }


    //    [HttpPost, ActionName("Delete")]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        await _jogoService.Remover(id);

    //        return RedirectToAction("Index");
    //    }


    //}
}
