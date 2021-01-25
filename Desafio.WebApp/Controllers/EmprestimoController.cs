using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Desafio.Business.Interfaces;
using AutoMapper;
using Desafio.Business.Models;

namespace Desafio.WebApp.Controllers
{
    [Authorize]
    public class EmprestimoController : Controller
    {
        //private readonly IEmprestimoService _emprestimoService;
        //private readonly IDesenvolvedorService _pessoaService;
        //private readonly IJogoService _jogoService;
        //private readonly IJogoRepository _jogoRepository;
        //private readonly IMapper _mapper;

        //public EmprestimoController(IEmprestimoService emprestimoService, IMapper mapper, IDesenvolvedorService pessoaService, IJogoService jogoService, IJogoRepository jogoRepository)
        //{
        //    _emprestimoService = emprestimoService;
        //    _mapper = mapper;
        //    _pessoaService = pessoaService;
        //    _jogoService = jogoService;
        //    _jogoRepository = jogoRepository;

        //}

        //public async Task<IActionResult> Index()
        //{
        //    return View(_mapper.Map<IEnumerable<EmprestimoViewModel>>(await _emprestimoService.ObterTodosEmprestimosPessoaJogo()));
        //}


        //public async Task<IActionResult> Details(int id)
        //{
        //    var emprestimoViewModel = _mapper.Map<EmprestimoViewModel>(await _emprestimoService.ObterEmprestimosPessoaJogo(id));

        //    return View(emprestimoViewModel);
        //}


        //public async Task<IActionResult> Create()
        //{
        //    var emprestimoViewModel = new EmprestimoViewModel();
            
        //    var listPessoas = _mapper.Map<IEnumerable<PessoaViewModel>>(await _pessoaService.BuscarTodos()).ToList();
        //    listPessoas.Insert(0, new PessoaViewModel {Nome = "Selecione" });

        //    emprestimoViewModel.Pessoas = listPessoas;

        //    var listJogos = _mapper.Map<IEnumerable<JogoViewModel>>(await _jogoRepository.Buscar(x => !x.Emprestado)).ToList();
        //    listJogos.Insert(0, new JogoViewModel { Nome = "Selecione" });

        //    emprestimoViewModel.Jogos = listJogos;
        //    emprestimoViewModel.DataEmprestimo = DateTime.Today;

        //    return View(emprestimoViewModel);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Create(EmprestimoViewModel emprestimoViewModel)
        //{
        //    if (!ModelState.IsValid) return View(emprestimoViewModel);

        //    var emprestimo = _mapper.Map<Emprestimo>(emprestimoViewModel);
        //    await _emprestimoService.Adicionar(emprestimo);


        //    return RedirectToAction("Index");
        //}


        //public async Task<IActionResult> Edit(int id)
        //{
        //    var emprestimoViewModel = _mapper.Map<EmprestimoViewModel>(await _emprestimoService.BuscarPorID(id));

        //    emprestimoViewModel.Pessoas = _mapper.Map<IEnumerable<PessoaViewModel>>(await _pessoaService.BuscarTodos()).ToList();
        //    emprestimoViewModel.Jogos = _mapper.Map<IEnumerable<JogoViewModel>>(await _jogoService.BuscarTodos()).ToList();

        //    emprestimoViewModel.DataEmprestimo = DateTime.Today;

        //    return View(emprestimoViewModel);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(EmprestimoViewModel emprestimoViewModel)
        //{
        //    if (!ModelState.IsValid) return View(emprestimoViewModel);


        //    var emprestimo = _mapper.Map<Emprestimo>(emprestimoViewModel);
        //    await _emprestimoService.Atualizar(emprestimo);

        //    return RedirectToAction("Index");
        //}


        //public async Task<IActionResult> Devolution(int id)
        //{
        //    var emprestimoViewModel = _mapper.Map<EmprestimoViewModel>(await _emprestimoService.ObterEmprestimosPessoaJogo(id));

        //    emprestimoViewModel.DataDevolucao = DateTime.Today;

        //    return View(emprestimoViewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Devolution(EmprestimoViewModel emprestimoViewModel)
        //{
        //    var emprestimo = _mapper.Map<Emprestimo>(emprestimoViewModel);
        //    await _emprestimoService.Devolver(emprestimo);

        //    return RedirectToAction("Index");
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var emprestimoViewModel = _mapper.Map<EmprestimoViewModel>(await _emprestimoService.ObterEmprestimosPessoaJogo(id));

        //    return View(emprestimoViewModel);
        //}

        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _emprestimoService.Remover(id);

        //    return RedirectToAction("Index");
        //}


    }
}
