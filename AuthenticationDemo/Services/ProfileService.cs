﻿using AuthenticationDemo.Interfaces;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemo.Services;

public class ProfileService
{
    private readonly IUserRepository _userRepository;

    public ProfileService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> GetProfileById(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return user;
    }
    public async Task<User> GetProfileEmail(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user;
    }
    public async Task<bool> EditProfile(string id, UserEdit userDto) //should just have a DTO instead of a User
    {
        var userToChange = await _userRepository.GetUserByIdAsync(id);
        userToChange.FirstName = userDto.FirstName;
        userToChange.LastName = userDto.LastName;
        userToChange.ProfilePicture = userDto.ProfilePicture;
        var res = await _userRepository.Update(userToChange);
        return res.Succeeded; 
    }
    public async Task<bool> ChangeProfilePicture(string id, string photo, string fileName, string isMain)
    {
        return true; //Not Implemented Yet "Should Save Photo to User Profile
    }
    public async Task<bool> SetStyles(List<int> Styles, string id)
    {
        return true; //Not Implemented Yet "Should Set Styles Here"
    }
    
    

}
