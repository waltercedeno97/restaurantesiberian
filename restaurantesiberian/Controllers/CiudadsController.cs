﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurantesiberian.Models;

namespace restaurantesiberian.Controllers
{
    [ApiController]
    public class CiudadsController : ControllerBase
    {
        private readonly SiberianDBContext _context;

        public CiudadsController(SiberianDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("api/GrabaCiudad")]
        public async Task<IActionResult> GrabaCiudad(InputCiudad ciudad)
        {
            Ciudad ciudadModels = new();
            RespuestaModels respuesta = new();
            try
            {
              ciudadModels.NombreCiudad = ciudad.NombreCiudad;
                ciudadModels.FechaCreacion = DateTime.Now;
                _context.Ciudads.Add(ciudadModels);
            await _context.SaveChangesAsync();

            respuesta.Status = 1;
        
            }
            catch (Exception e)
            {
                respuesta.Status = 0;
                respuesta.Mensaje = e.Message;
                // return Ok(respuesta);
            }
             return Ok(respuesta);
        }

        [HttpPost]
        [Route("api/ActualizaCiudad")]
        public  IActionResult ActualizaCiudad(InputCiudad ciudad)
        {

            RespuestaModels respuesta = new();
            try
            {
                Ciudad ciudadModels = _context.Ciudads.Find(ciudad.Idciudad);
               // ciudadModels.Idciudad = ciudad.Idciudad;
                ciudadModels.NombreCiudad = ciudad.NombreCiudad;
                _context.Entry(ciudadModels).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                respuesta.Status = 1;

            }
            catch (Exception e)
            {
                respuesta.Status = 0;
                respuesta.Mensaje = e.Message;
                // return Ok(respuesta);
            }
            return Ok(respuesta);
        }


    }
}
