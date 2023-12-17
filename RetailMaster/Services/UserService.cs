using System.Data;
using MySqlConnector;

namespace RetailMaster.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RetailMaster.DTO;
using RetailMaster.Models;

public class UserService
{
    private readonly RetailMasterContext _context;
    private readonly IMapper _mapper;

    public UserService(RetailMasterContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetUserById(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }
    
    public async Task<UserDto?> GetUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<bool> DeleteUser(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
    
    // call procedure with parameters and return a table with results
    public DataTable CallProcedure(string procedureName, params MySqlParameter[] parameters) {
        var command = _context.Database.GetDbConnection().CreateCommand();
        command.CommandText = procedureName;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(parameters);
        
        var adapter = new MySqlDataAdapter((MySqlCommand)command);
        var table = new DataTable();
        adapter.Fill(table);

        return table;
    }

}
