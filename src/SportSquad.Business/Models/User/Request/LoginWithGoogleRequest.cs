﻿namespace SportSquad.Business.Models.User.Request;

public class LoginWithGoogleRequest
{
    public string Provider { get; set; }
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AuthToken { get; set; }
    public string IdToken { get; set; }
    public string AuthorizationCode { get; set; }
    public object Response { get; set; }
}
