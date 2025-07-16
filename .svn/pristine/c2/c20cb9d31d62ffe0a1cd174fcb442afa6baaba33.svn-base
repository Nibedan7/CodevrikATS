using CdplATS.Entity.Models;
using CdplATS.API.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CdplATS.API.Services
{
    public class UserService
    {
        private readonly GenericRepository _genericRepository;

        public UserService(GenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ApiResponse<IEnumerable<LoggerEntity>>> GetAllUsers()
        {
            try
            {
                var users = await _genericRepository.GetAllAsync<LoggerEntity>("GetAllUsers");
                return new ApiResponse<IEnumerable<LoggerEntity>>(users, "Users retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<LoggerEntity>>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<LoggerEntity>> GetUserById(int userId)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId", SqlDbType.Int) { Value = userId }
                };

                var user = await _genericRepository.GetAsync<LoggerEntity>("GetUserById", parameters);
                return new ApiResponse<LoggerEntity>(user, "User retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoggerEntity>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<LoggerEntity>> GetUserByIdAndName(int userId, string userName)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId", SqlDbType.Int) { Value = userId },
                    new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = userName }
                };

                var user = await _genericRepository.GetAsync<LoggerEntity>("GetUserByIdAndName", parameters);
                return new ApiResponse<LoggerEntity>(user, "User retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoggerEntity>(null, $"Error: {ex.Message}", false);
            }
        }

        //public async Task<ApiResponse<int>> CreateUserAsync(LoggerEntity user)
        //{
        //    try
        //    {
        //        var parameters = new SqlParameter[]
        //        {
             
        //            new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = user.Email },
        //            new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 256) { Value = user.PasswordHash }
        //        };

        //        var result = await _genericRepository.ExecuteAsync("CreateUser", parameters);
        //        return new ApiResponse<int>(result, "User created successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<int>(0, $"Error: {ex.Message}", false);
        //    }
        //}

        //public async Task<ApiResponse<int>> UpdateUserAsync(LoggerEntity user)
        //{
        //    try
        //    {
        //        var parameters = new SqlParameter[]
        //        {
        //            new SqlParameter("@UserId", SqlDbType.Int) { Value = user.Id },
                
        //            new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = user.Email },
        //            new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 256) { Value = user.PasswordHash }
        //        };

        //        var result = await _genericRepository.ExecuteAsync("UpdateUser", parameters);
        //        return new ApiResponse<int>(result, "User updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<int>(0, $"Error: {ex.Message}", false);
        //    }
        //}

        public async Task<ApiResponse<int>> DeleteUser(int userId)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId", SqlDbType.Int) { Value = userId }
                };

                var result = await _genericRepository.ExecuteAsync("DeleteUser", parameters);
                return new ApiResponse<int>(result, "User deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(0, $"Error: {ex.Message}", false);
            }
        }
    }
}
