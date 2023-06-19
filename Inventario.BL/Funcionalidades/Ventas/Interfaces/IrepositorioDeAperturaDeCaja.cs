﻿using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    public interface IrepositorioDeAperturaDeCaja
    {
        public void CrearUnaAperturaDeCaja(AperturaDeCaja aperturaDeCaja);

        public IEnumerable<AperturaDeCaja> ListarAperturasDeCaja();

        public IEnumerable<AperturaDeCaja> AperturasDeCajaPorUsuario(int idUsuario);

        public AperturaDeCaja ObtenerUnaAperturaDeCajaPorId(int id);

        public void CerrarUnaAperturaDeCaja(int id);

    }
}
