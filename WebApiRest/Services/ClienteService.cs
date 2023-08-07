using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WebApiRest.Factory;
using WebApiRest.Models;

namespace WebApiRest.Services
{
    public interface IClienteService
    {
        Response AgregarCliente(Cliente cliente);
        Response ActualizarCliente(long clienteId, Cliente cliente);
        Response EliminarCliente(long clienteId);
        IEnumerable<Cliente> ObtenerClientes();
        Cliente ObtenerCliente(long clienteId);
    }
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IFactory factory)
        {
            _clienteRepository = factory.CreateClienteRepository();
        }
        public Response ActualizarCliente(long clienteId, Cliente clienteActualizar)
        {
            if (clienteId != clienteActualizar.Id)
                return new Response(HttpStatusCode.BadRequest, "Los Ids no coinciden", clienteActualizar);
            var cliente = _clienteRepository.ObtenerCliente(clienteId);
            if (cliente == null)
                return new Response(HttpStatusCode.NotFound, "No existe el cliente", cliente);
            if (cliente.Cedula.Trim() == "" || cliente.Direccion.Trim() == "" || cliente.Nombres.Trim() == "" || cliente.Telefono.Trim() == "")
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", clienteActualizar);
            if (_clienteRepository.CantidadClientes(clienteActualizar.Cedula) > 0 && cliente.Cedula.ToLower().Trim() != clienteActualizar.Cedula.ToLower().Trim())
                return new Response(HttpStatusCode.BadRequest, "El número de documento de identificación ya se ha usado", clienteActualizar);
            try
            {
                _clienteRepository.ActualizarCliente(clienteId, clienteActualizar);
                return new Response(HttpStatusCode.OK, "Cliente actualizado", clienteActualizar);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.NotModified, ex.Message, clienteActualizar);
            }
        }

        public Response AgregarCliente(Cliente cliente)
        {
            if (cliente.Cedula.Trim() == "" || cliente.Direccion.Trim() == "" || cliente.Nombres.Trim() == "" || cliente.Telefono.Trim() == "")
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", cliente);
            if (_clienteRepository.ExisteCliente(cliente.Cedula))
                return new Response(HttpStatusCode.BadRequest, "El número de documento de identificación ya existe", cliente);
            try
            {
                _clienteRepository.CrearCliente(cliente);
                return new Response(HttpStatusCode.Created, "Cliente creado", cliente);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, cliente);
            }
        }

        public Response EliminarCliente(long clienteId)
        {
            var cliente = _clienteRepository.ObtenerCliente(clienteId);
            if (cliente == null)
                return new Response(HttpStatusCode.NotFound, "No existe el cliente", cliente);
            try
            {
                _clienteRepository.EliminarCliente(clienteId);
                return new Response(HttpStatusCode.OK, "Cliente eliminado", cliente);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, cliente);
            }
        }

        public Cliente ObtenerCliente(long clienteId)
        {
            return _clienteRepository.ObtenerCliente(clienteId);
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            return _clienteRepository.ObtenerClientes();
        }
    }
}