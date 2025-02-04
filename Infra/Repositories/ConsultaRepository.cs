﻿using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly Context _context;

        public ConsultaRepository(Context context)
        {
            _context = context;
        }

        public async Task<Consulta> GetConsultaByHorarioAsync(int idMedico, int idHorarioDisponivel)
        {
            
            return await _context.Consultas
                .FromSqlRaw("SELECT * FROM \"Consultas\" WHERE \"IdMedico\" = {0} AND \"IdHorarioDisponivel\" = {1} AND (\"IsAceita\" IS NULL OR \"IsAceita\" = TRUE) FOR UPDATE", idMedico, idHorarioDisponivel)
                .FirstOrDefaultAsync();
        }


        public async Task AddAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }
    }
}
