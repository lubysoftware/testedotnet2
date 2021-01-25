using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Desafio.WebApp.Areas.Identity.Data;
using Desafio.WebApp.ViewModels;
using Desafio.Business.Interfaces;
using AutoMapper;
using Desafio.Business.Models;
using Microsoft.AspNetCore.Authorization;

namespace Desafio.WebApp.Controllers
{
    //[Authorize]
    //public class PessoaController : Controller
    //{
    //    private readonly IDesenvolvedorService _pessoaService;
    //    private readonly IEmprestimoService _emprestimoService;
    //    private readonly IPessoaRepository _pessoaRepository;
    //    private readonly IMapper _mapper;

    //    public PessoaController(IDesenvolvedorService pessoaService, IMapper mapper, IPessoaRepository pessoaRepository, IEmprestimoService emprestimoService)
    //    {
    //        _pessoaService = pessoaService;
    //        _mapper = mapper;
    //        _pessoaRepository = pessoaRepository;
    //        _emprestimoService = emprestimoService;
    //    }




    //    public async Task<IActionResult> Index()
    //    {
    //        return View(_mapper.Map<IEnumerable<PessoaViewModel>>(await _pessoaService.BuscarTodos()));
    //    }


    //    public async Task<IActionResult> Details(int id)
    //    {
    //        var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaService.BuscarPorID(id));

    //        pessoaViewModel.Emprestimo = _mapper.Map<IEnumerable<EmprestimoViewModel>>(await _emprestimoService.ObterEmprestimosPessoa(id));

    //        return View(pessoaViewModel);
    //    }


    //    public IActionResult Create()
    //    {
    //        return View();
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Create(PessoaViewModel pessoaViewModel)
    //    {
    //        if (!ModelState.IsValid) return View(pessoaViewModel);

    //        var pessoa = _mapper.Map<Pessoa>(pessoaViewModel);
    //        await _pessoaService.Adicionar(pessoa);

    //        return RedirectToAction("Index");
    //    }


    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaService.BuscarPorID(id));

    //        return View(pessoaViewModel);
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Edit(PessoaViewModel pessoaViewModel)
    //    {
    //        if (!ModelState.IsValid) return View(pessoaViewModel);


    //        var pessoa = _mapper.Map<Pessoa>(pessoaViewModel);
    //        await _pessoaService.Atualizar(pessoa);

    //        return RedirectToAction("Index");
    //    }


    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaService.BuscarPorID(id));

    //        return View(pessoaViewModel);
    //    }


    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        await _pessoaService.Remover(id);

    //        return RedirectToAction("Index");
    //    }


    //}
}
