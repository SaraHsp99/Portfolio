using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Entities.Account;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services.Account;
using Portfolio.Core.Interfaces.Services.Account.Dtos;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Core.Securities;
using Portfolio.Infrastructure;
using Portfolio.Provider.CacheProvider;
using Portfolio.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Core.Common;

namespace Portfolio.Provider.AccountProvider;
public class UserService : BaseProvider, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        ICacheService cacheService,
        IResult result,
        IUserRepository userRepository)
        : base( mapper, cacheService, result)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult> CreateUser(RegisterDto registerDto)
    {

        var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            _result.Rsl = false;
            _result.Message = "User with this email already exists.";
            return _result;
        }

        var createDate = DateTime.Now;
        var user = _mapper.Map<User>(registerDto);
        var passwordDto = PasswordManagment.HashingPassword(registerDto.StrPassword);
        user.Password = passwordDto.Password;
        user.SaltPassword = passwordDto.SaltPassword;
        user.CreateDate = createDate;
        user.IsActive = true;
        user.IsLock = false;


        await _userRepository.AddUserAsync(user);

        _result.Rsl = true;
        _result.Message = "User created successfully.";
        return _result;
    }
    public LoginResultDto Login(LoginDto input)
    {
        var loginResult = new LoginResultDto();
        var user = _userRepository.GetUserByUserNameAsync(input.UserName);

        if (user is null)
        {
            loginResult.LoginResult = LoginResult.NotExistUser;
			_result.Rsl = false;
			_result.Message = AllMessage.UserNameNotExistUser;
            loginResult.Result = (Result)_result;
			return loginResult;
        }
        var verifyPass = PasswordManagment.VerifyPassword(input.StrPassword, user.Result.Password, user.Result.SaltPassword);

        if (!verifyPass)
        {
            loginResult.LoginResult = LoginResult.IncorrecrPassword;
            _result.Rsl = false;
            _result.Message = AllMessage.UserNameNotExistUser;

        }

        else if (user.Result.IsLock)
        {
            loginResult.LoginResult = LoginResult.IsLock;
            _result.Rsl = false;
            _result.Message = AllMessage.IsLock;
        }


        else if (!user.Result.IsActive ?? true)
        {
            loginResult.LoginResult = LoginResult.NotActive;
            _result.Rsl = false;
            _result.Message = AllMessage.NotActive;
        }


        else
        {
            loginResult.LoginResult = LoginResult.Success;
            loginResult.UserDto = _mapper.Map<UserDto>(user);

        }
        LoginInfo(user.Result, loginResult.LoginResult);
        loginResult.Result = (Result)_result;
        return loginResult;
    }
    private void LoginInfo(User user, LoginResult login)
    {
        if (login == LoginResult.Success)
        {
            user.CountFailLogin = 0;
          
        }
        else if (login == LoginResult.NotConfirmPassword)
        {
            //   _mySession.SetSession("MsgLastLogin", $"آخرین ورود شما{DateManagment.MiladiToShamsi(user.LastDateLogin)}");

            user.CountFailLogin = (short)((user.CountFailLogin ?? 0) + 1);
            user.LastDateLogin = DateTime.Now;

        }

    }

}



//public class UserService : BaseProvider, IUserService
//{
//    private readonly IGeneralServices _generalServices;
//    private readonly IPhyService _phyService;
//    private readonly IClinicManagmentService _clinicManagmentService;
//    private readonly IRoleService _roleService;
//    private readonly IPhyClncsService _phyClncsService;
//    private readonly IMelipayamakService _melipayamakService;
//    private readonly IPayamakService _payamakService;


//    public UserService(BshisDbContext db, IMapper mapper, IResult result, IMysession mySession, ICacheService cacheService, IPhyService phyService, IGeneralServices generalServices, IClinicManagmentService clinicManagmentService, IRoleService roleService, IPhyClncsService phyClncsService, IMelipayamakService melipayamakService, IPayamakService payamakService) : base(db, mapper, result, mySession, cacheService)
//    {
//        _phyService = phyService;
//        _generalServices = generalServices;
//        _clinicManagmentService = clinicManagmentService;
//        _roleService = roleService;
//        _phyClncsService = phyClncsService;
//        _melipayamakService = melipayamakService;
//        _payamakService = payamakService;
//    }
//    public IResult AddJustUser(UserDto dto)
//    {
//        try
//        {
//            var createBy = _mySession.UserId;
//            var createDate = DateTime.Now;
//            var user = _mapper.Map<User>(dto);
//            var passwordDto = PasswordManagment.HashingPassword(dto.StrPassword);
//            user.Password = passwordDto.Password;
//            user.SaltPassword = passwordDto.SaltPassword;
//            user.CreateBy = createBy;
//            user.CreateDate = createDate;
//            var userEntity = _db.User.Add(user);
//            _db.SaveChanges();
//            _result.Data = userEntity.Entity;
//            return _result;
//        }
//        catch (Exception ex)
//        {

//            throw;
//        }

//    }
//    public IResult UpdateJustUser(UserDto userDto)
//    {
//        var user = _db.User
//           .Include(x => x.Prn)
//           .FirstOrDefault(x => x.Id == userDto.Id);
//        user.FirstName = userDto.FirstName;
//        user.LastName = userDto.LastName;
//        user.IsActive = userDto.IsActive;
//        user.Email = userDto.Email;
//        user.UserName = userDto.UserName;
//        user.PhoneNumber = userDto.PhoneNumber;
//        user.Prn.Nty = userDto.Nty;
//        user.Prn.Fnm = userDto.FirstName;
//        user.Prn.Lnm = userDto.LastName;
//        user.Prn.FthNm = userDto.FatherName;
//        user.Prn.NtyId = userDto.UserName;
//        user.Prn.Sex = userDto.sex;
//        _db.User.Update(user);
//        _db.SaveChanges();
//        _result.Data = user;
//        return _result;
//    }
//    public LoginViewModel Login(LoginDto input)
//    {

//        LoginViewModel loginViewModel = new LoginViewModel();


//        var user = _db.User
//           .FirstOrDefault(x => x.Email == input.UserName || x.UserName == input.UserName);
//        if (user == null)
//        {
//            loginViewModel.LoginResult = LoginResult.NotExistUser;
//            return loginViewModel;
//        }
//        var verifyPass = PasswordManagment.VerifyPassword(input.StrPassword, user.Password, user.SaltPassword);

//        if (!verifyPass)
//        {
//            loginViewModel.LoginResult = LoginResult.IncorrecrPassword;
//            loginViewModel.userDto = _mapper.Map<UserDto>(user);

//        }
//        else if (user.IsLock)
//            loginViewModel.LoginResult = LoginResult.IsLock;
//        else if (!user.IsActive == true)
//            loginViewModel.LoginResult = LoginResult.NotActive;
//        else
//        {
//            loginViewModel.LoginResult = LoginResult.Success;
//            loginViewModel.userDto = _mapper.Map<UserDto>(user);
//            loginViewModel.userDto.OrgnId = input.OrgnId;

//        }
//        LoginInfo(user, loginViewModel.LoginResult);
//        return loginViewModel;
//    }
//    private void LoginInfo(User user, LoginResult login)
//    {
//        if (login == LoginResult.Success)
//        {
//            user.CountFailLogin = 0;
//            _db.SaveChanges();
//        }
//        else if (login == LoginResult.NotConfirmPassword)
//        {
//            _mySession.SetSession("MsgLastLogin", $"آخرین ورود شما{DateManagment.MiladiToShamsi(user.LastDateLogin)}");

//            user.CountFailLogin = (short)((user.CountFailLogin ?? 0) + 1);
//            user.LastDateLogin = DateTime.Now;
//            _db.SaveChanges();
//        }

//    }

//    public bool ExistEmailOrUsername(string email, string userName)
//    {
//        var user = GetUserByEmailOrUserName(email, userName);
//        if (user == null) return false;
//        else return true;
//    }
//    public UserDto GetUserByEmailOrUserName(string email, string userName, bool isFirstRegister = true)
//    {
//        if (isFirstRegister == false)
//        {
//            return null;
//        }
//        var user = _db.User
//            .FirstOrDefault(x => x.Email == email || x.UserName == userName);
//        var userDto = _mapper.Map<UserDto>(user);
//        return userDto;
//    }
//    public PhyDto GetPhyByUserId(long userId)
//    {
//        var phy = _db.User.Where(x => x.Id == userId).Select(x => x.Prn.Phy).FirstOrDefault();
//        if (phy != null)
//        {
//            var phydto = _mapper.Map<PhyDto>(phy);
//            if (phydto.Spy != null)
//            {
//                var spyInfo = _generalServices.GetSpecialityBySpyId(phy.Spy.Value);
//                phydto.SpyNm = spyInfo.Title;
//                phydto.SpyImg = (long?)spyInfo.PrcVal ?? (long?)0;
//            }
//            return phydto;
//        }
//        return null;
//    }
//    public List<UserDto> GetAllUser()
//    {
//        if (_mySession.SuperAdmin == "SuperAdmin@Rahya.com")
//        {
//            var allUser = _db.User
//            .Include(x => x.UserRole)
//            .ThenInclude(x => x.Role)
//            .AsNoTracking()
//            .ToList();
//            return _mapper.Map<List<UserDto>>(allUser);
//        }

//        var prnId = _mySession.PrnId;
//        var orgnId = _mySession.OrgnId;
//        var phyClncsId = _db.PhyClncs.Where(x => x.OrgnId == orgnId).Select(x => x.Id).ToArray();
//        var prnClcPrnIdList = _db.PrnClc.Where(x => phyClncsId.Contains(x.Phyclcid.Value)).Select(x => x.Prnid).ToArray();
//        var Users = _db.User
//            .Where(x => prnClcPrnIdList.Contains(x.PrnId))
//            .Include(x => x.UserRole)
//            .ThenInclude(x => x.Role)
//            .AsNoTracking()
//            .ToList();
//        return _mapper.Map<List<UserDto>>(Users);
//    }
//    public UserDto GetUserById(int id)
//    {
//        var user = _db.User
//            .Include(x => x.Prn)
//            .ThenInclude(x => x.PrnClc)
//            .Include(x => x.UserRole)
//            .FirstOrDefault(x => x.Id == id);
//        var UserDto = _mapper.Map<UserDto>(user);
//        UserDto.FatherName = user.Prn.FthNm;
//        UserDto.sex = user.Prn.Sex;
//        UserDto.Nty = user.Prn.Nty;
//        UserDto.RoleIdList = user.UserRole.Select(x => x.RoleId.Value).ToList();
//        UserDto.PhyClcIdList = user.Prn.PrnClc.Select(x => x.Phyclcid.Value).ToList();
//        return UserDto;
//    }
//    public IResult StatusActiveUser(long id)
//    {
//        var user = _db.User.FirstOrDefault(x => x.Id == id);
//        user.IsActive = !user.IsActive;
//        _db.User.Update(user);
//        _db.SaveChanges();
//        _result.Data = user.IsActive;
//        _result.Message = AllMessage.EditSuccess;
//        _result.Rsl = true;
//        return _result;
//    }
//    public UserDto GetUserByPrnId(long prnId)
//    {
//        var user = _db.User.FirstOrDefault(x => x.PrnId == prnId);
//        return
//             _mapper.Map<UserDto>(user);
//    }
//    public IResult RegisterUserPhy(UserDto input)
//    {
//        //    var nationalcode = Convert.ToInt64(input.UserName);
//        //    var resultDhahlar =_webService. ShakarService(nationalcode, input.PhoneNumber);
//        //    if (resultDhahlar.Rsl == true) {
//        input.RoleIdList = _db.Role.Where(x => x.RoleName == RoleNames.Physician).Select(x => x.Id).ToList();
//        return AddUser(input, true);
//        //}
//        //return resultDhahlar;
//    }
//    public void AddUserAllPermisioan(long userId)
//    {
//        var permissions = _db.Permission.AsNoTracking().ToList();
//        if (permissions.Any())
//        {
//            var userPermissionDtos = new List<UserPermissionDto>();
//            permissions.ForEach(x =>
//            {
//                userPermissionDtos.Add(new UserPermissionDto
//                {
//                    IsGranted = false,
//                    UserId = userId,
//                    PermissionId = x.Id
//                });
//            });
//            var userPermissions = _mapper.Map<List<UserPermission>>(userPermissionDtos);
//            _db.UserPermission.AddRange(userPermissions);
//            _db.SaveChanges();
//        }
//    }
//    public void AddUserRoleList(List<int> roleIds, long userId)
//    {
//        var userRoleDtos = new List<UserRoleDto>();
//        roleIds.ForEach(x =>
//        {
//            userRoleDtos.Add(new UserRoleDto
//            {
//                UserId = userId,
//                RoleId = x
//            });
//            if (_db.RolePermission.Any(r => r.RoleId == x && r.IsGranted == true))
//            {
//                var rolePermissionListIsGranted = _db.RolePermission.Where(r => r.RoleId == x && r.IsGranted == true).ToList();
//                rolePermissionListIsGranted.ForEach(r =>
//                {
//                    var userPermission = _db.UserPermission.FirstOrDefault(x => x.UserId == userId && x.PermissionId == r.PermissionId);
//                    userPermission.IsGranted = true;
//                    _db.UserPermission.Update(userPermission);
//                });
//            }
//        });
//        var userRoles = _mapper.Map<List<UserRole>>(userRoleDtos);
//        _db.UserRole.AddRange(userRoles);
//        _db.SaveChanges();
//    }
//    public void RemovePrnClinicsList(long? prnId)
//    {
//        var prnClncsList = _db.PrnClc.Where(x => x.Prnid == prnId).ToList();
//        _db.PrnClc.RemoveRange(prnClncsList);
//        _db.SaveChanges();
//    }
//    public void AddPrnClinicsList(List<long> Phyclcids, long prnId)
//    {
//        var phyId = _mySession.PrnId;
//        var insBy = _mySession.UserId;
//        if (Phyclcids?.Count() != 0 && Phyclcids != null)
//        {
//            Phyclcids.ForEach(phyclc =>
//            {
//                _db.PrnClc.Add(new PrnClc
//                {
//                    Phyclcid = phyclc,
//                    Prnid = prnId,
//                    InsD = DateTime.Now,
//                    InsBy = insBy,
//                });
//                _db.SaveChanges();
//            });
//        }
//    }
//    public void AddPhyClinicsList(long phyId, List<int> orgnIds)
//    {
//        orgnIds.ForEach(orgn =>
//        {
//            _phyClncsService.AddPhyClc(phyId, orgn);
//        });
//    }

//    public IResult AddPrnAndGetPrnId(PrnnDto input)
//    {
//        try
//        {
//            var prnEntity = _db.Prnn.Add(new Prnn
//            {
//                Fnm = input.Fnm,
//                Lnm = input.Lnm,
//                FthNm = input.FthNm,
//                NtyId = input.NtyId,
//                Sex = input.Sex,
//                Email = input.Email,
//                Nty = input.Nty
//            });
//            _db.SaveChanges();
//            _result.Data = prnEntity.Entity.Id;
//            return _result;
//        }
//        catch (Exception ex)
//        {

//            throw;
//        }

//    }
//    public IResult AddUser(UserDto input, bool isFirstRegister = true)
//    {
//        var validationNationalID = ValidationNationalID.IsValidNationalCode(input.UserName);
//        if (!validationNationalID.Rsl)
//        {
//            _result.Rsl = false;
//            _result.Message = validationNationalID.Message;
//            return _result;
//        }
//        var userDto = GetUserByEmailOrUserName(input.Email, input.UserName, isFirstRegister);
//        if (!CheckUserExistClinic(isFirstRegister, input.PhyClcIdList, input.UserName).Rsl)
//            return CheckUserExistClinic(isFirstRegister, input.PhyClcIdList, input.UserName);
//        if (userDto?.PrnId == null)
//        {
//            input.PrnId = (long)AddPrnAndGetPrnId(new PrnnDto
//            {
//                Fnm = input.FirstName,
//                Lnm = input.LastName,
//                FthNm = input.FatherName,
//                NtyId = input.UserName,
//                Sex = input.sex,
//                Email = input.Email,
//                Nty = input.Nty
//            }).Data;
//        }
//        input.PrnId = input.PrnId ?? userDto?.PrnId;
//        if (!ExistEmailOrUsername(input.Email, input.UserName))
//        {
//            var user = (User)AddJustUser(input).Data;
//            var userId = user.Id;
//            AddUserAllPermisioan(userId);
//            AddUserRoleList(input.RoleIdList, userId);
//            if (isFirstRegister == false)
//            {
//                AddPrnClinicsList(input.PhyClcIdList, input.PrnId.Value);

//                if (input.RoleIdList.Any(x => x == (int)RoleEnum.physicianId))
//                {
//                    _phyService.AddPhy(new PhyUserDto
//                    {
//                        PrnId = user.PrnId.Value,
//                    });
//                }
//            }
//            _result.Data = new ResultRegisterUser
//            {
//                prnId = input.PrnId,
//                resultUserEnum = (int)RegisterResult.Success
//            };
//            _result.Message = AllMessage.SaveSuccess;
//        }
//        else
//        {
//            _result.Message = AllMessage.UserIsExist;
//            _result.Data = new ResultRegisterUser
//            {
//                prnId = input.PrnId,
//                resultUserEnum = (int)RegisterResult.UserExist,
//            };
//            _result.Rsl = false;
//        }

//        return _result;
//    }
//    public IResult UpdateUser(UserDto userDto)
//    {
//        var validationNationalID = ValidationNationalID.IsValidNationalCode(userDto.UserName.Trim());
//        if (!validationNationalID.Rsl)
//        {
//            _result.Rsl = false;
//            _result.Message = validationNationalID.Message;
//            return _result;
//        }
//        var insBy = _mySession.UserId;
//        var user = (User)UpdateJustUser(userDto).Data;
//        var userRoleList = _db.UserRole
//           .Include(x => x.Role)
//           .ThenInclude(x => x.RolePermission)
//           .Include(x => x.User)
//           .ThenInclude(x => x.UserPermission)
//           .Where(x => x.UserId == userDto.Id)
//           .ToList();
//        UpdatUserPermissionIsGrantedToFalse(userRoleList);
//        RemoveUserRoleList(userRoleList);
//        AddUserRoleList(userDto.RoleIdList, user.Id);
//        RemovePrnClinicsList(user.PrnId);
//        AddPrnClinicsList(userDto.PhyClcIdList, userDto.PrnId.Value);
//        if (userDto.RoleIdList.Any(x => x == (int)RoleEnum.physicianId))
//        {
//            AddRolePhyToUser(user.PrnId.Value, userDto.PhyClcIdList);
//        }
//        else
//        {
//            CheckRemovePhy(user.PrnId.Value);
//        }
//        _result.Data = user;
//        _result.Message = AllMessage.EditSuccess;
//        return _result;
//    }
//    public void CheckRemovePhy(long phyId)
//    {
//        if (CheckPhyByPhyId(phyId))
//        {
//            _phyService.DeActivePhyByPhyId(phyId);
//        }
//    }
//    public bool CheckPhyByPhyId(long phyId)
//    {
//        return
//        _phyService.CheckPhyByPhyId(phyId);
//    }
//    public UserInfoDto GetUserInfo()
//    {
//        return new UserInfoDto
//        {
//            PrnId = _mySession.PrnId ?? 0,
//            UserId = _mySession.UserId ?? 0
//        };
//    }
//    public IResult AddRolePhyToUser(long prnId, List<long> phyClcIdList)
//    {
//        _phyService.AddPhy(new PhyUserDto
//        {
//            PrnId = prnId,
//        });
//        var orgnIds = _db.PhyClncs.Where(x => phyClcIdList.Contains(x.Id)).Select(x => x.OrgnId.Value).ToList();
//        AddPhyClinicsList(prnId, orgnIds);
//        return _result;
//    }
//    public IResult RegisterPhy(PhyUserDto dto)
//    {
//        return
//        _clinicManagmentService.RegisterPhy(dto);
//    }
//    public IResult AddUserClinic(OrgnUserDto orgnDto)
//    {
//        return
//              _clinicManagmentService.AddUserClinic(orgnDto);
//    }
//    public List<ItemListDto> GetActivePersonalClinicsByPrnId(long prnId)
//    {
//        return
//        _clinicManagmentService.GetActivePersonalClinicsByPrnId(prnId).Select(s => new ItemListDto
//        {
//            Text = s.Text,
//            Value = s.Value,
//        }).ToList();
//    }
//    public List<ItemListDto> GetAllRole()
//    {
//        return
//        _roleService.GetAllRole()
//     .Select(s => new ItemListDto
//     {
//         Text = s.Description,
//         Value = s.Id,
//     })
//     .ToList();
//    }
//    public List<ItemListDto> Getspecialty()
//    {
//        return
//        _cacheService.GetDlookupCache()
//            .Where(x => x.GrpId == (int)DlookUpEnum.specialty)
//            .Select(x => new ItemListDto
//            {
//                Text = x.Title,
//                Value = (int)x.IntVal.Value
//            })
//            .ToList();
//    }
//    public List<ItemListDto> GetPersonalClinicsByPrnId(long prnId)
//    {
//        return
//        _generalServices.GetPersonalClinicsByPrnId(prnId)
//           .Select(s => new ItemListDto
//           {
//               Text = s.Text,
//               Value = s.Value,
//           })
//           .ToList();
//    }
//    public List<ItemListDto> GetGender()
//    {
//        return
//        _generalServices.GetGender().Where(x => x.StrVal != "U" && x.StrVal != "O")
//           .Select(x => new ItemListDto
//           {
//               Text = x.Title,
//               ValueText = x.StrVal
//           }).ToList();
//    }
//    public List<ItemListDto> GetNationality()
//    {
//        return
//       _generalServices.GetNationality().Where(x => x.StrVal != "IRN")
//          .Select(x => new ItemListDto
//          {
//              Text = x.Title,
//              ValueText = x.StrVal
//          })
//          .ToList();
//    }
//    public List<ItemListDto> GetState()
//    {
//        return
//            _generalServices.GetState()
//            .Select(s => new ItemListDto
//            {
//                Text = s.Title,
//                ValueText = s.IntVal,

//            })
//            .ToList();
//    }
//    public List<ItemListDto> GetCity(string intVal)
//    {
//        return
//            _generalServices.GetCity(intVal)
//         .Select(s => new ItemListDto
//         {
//             Text = s.Title,
//             ValueText = s.StrVal
//         })
//            .ToList();
//    }
//    #region Private Method
//    private IResult CheckUserExistClinic(bool isFirstRegister, List<long> phyClcIdList, string userName)
//    {
//        if (isFirstRegister == false)
//        {
//            var i = 0;
//            phyClcIdList.ForEach(x =>
//            {
//                var check = CheckUserOnClinic(userName, x);
//                if (check == true)
//                {
//                    i++;
//                }
//            });
//            if (i > 0)
//            {
//                _result.Rsl = false;
//                _result.Message = AllMessage.PrnClinicError;
//                return _result;
//            }
//        }
//        return _result;
//    }
//    private bool CheckUserOnClinic(string ntyId, long clinicId)
//    {
//        var rec = _db.PrnClc.Any(x => x.Prn.NtyId == ntyId && x.Phyclc.OrgnId == clinicId);
//        return rec;
//    }
//    private void RemoveUserRoleList(List<UserRole> userRoleList)
//    {
//        _db.UserRole.RemoveRange(userRoleList);
//        _db.SaveChanges();
//    }
//    private void UpdatUserPermissionIsGrantedToFalse(List<UserRole> userRoleList)
//    {
//        userRoleList.ForEach(x =>
//        {
//            var permisionIdList = x.Role.RolePermission
//            .Select(x => x.PermissionId)
//            .ToArray();
//            var userPermissionList = x.User.UserPermission
//            .Where(x => permisionIdList
//            .Contains(x.PermissionId) && x.IsGranted == true).ToList();
//            userPermissionList.ForEach(y =>
//            {
//                y.IsGranted = false;
//            });
//            _db.UserPermission.UpdateRange(userPermissionList);
//        });
//        _db.SaveChanges();
//    }
//    #endregion

//    public IResult ChangePass(ChangePassDto changePass)
//    {
//        if (changePass.NewPass == changePass.CurrentPass)
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.NotEqualCurrentPassWithNewPass;
//            return _result;
//        }

//        if (changePass.NewPass != changePass.NewRePass)
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.IncorrecrRePassword;
//            return _result;
//        }


//        var user = _db.User
//            .First(x => x.Email == _mySession.Email);

//        if (!PasswordManagment.VerifyPassword(changePass.CurrentPass, user.Password, user.SaltPassword))
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.IncorrecrPassword;
//            return _result;
//        }


//        var pass = PasswordManagment.HashingPassword(changePass.NewPass);
//        user.Password = pass.Password;
//        user.SaltPassword = pass.SaltPassword;
//        _db.SaveChanges();
//        _result.Rsl = true;
//        _result.Message = AllMessage.SaveSuccess;
//        return _result;

//    }

//    public void SaveUserLoginAttempt(string browserInfo, string ip, long? userId, string userName, bool loginSuccess)
//    {
//        //var strDns=string.Empty;
//        //try
//        //{
//        //    strDns = Dns.GetHostEntry(ip).HostName;
//        //}
//        //catch 
//        //{
//        //    strDns = null;

//        //}
//        _db.UserLoginAttempt.Add(new UserLoginAttempt
//        {
//            BrowserInfo = browserInfo,
//            ClientIpAddress = ip,
//            ClientName = null,
//            Date = DateTime.Now,
//            IsLogin = loginSuccess,
//            UserId = userId,
//            UserName = userName,
//        });
//        _db.SaveChanges();
//    }

//    public List<UserLoginAttemptDto> AllUserLoginAttempt(string? datePersian)
//    {
//        var dateMiladi = DateManagment.ShamsiToMiladiWithFullDate(datePersian);
//        var addDay = datePersian is null ? 7 : 0;
//        var records = _db.UserLoginAttempt
//        .Join(
//            inner: _db.User,
//            outerKeySelector: u => u.UserId,
//            innerKeySelector: u0 => (long?)u0.Id,
//            resultSelector: (u, u0) => new { Outer = u, Inner = u0 }
//        )
//        .Where(ti => ti.Outer.Date.Date == dateMiladi.Date)
//        .OrderByDescending(ti => ti.Outer.Date.AddDays(addDay).Date <= dateMiladi.Date)
//        .Take(10)
//        .Select(ti => new UserLoginAttemptDto
//        {
//            ClientIpAddress = ti.Outer.ClientIpAddress,
//            IsLogin = ti.Outer.IsLogin,
//            UserId = ti.Outer.UserId,
//            UserName = ti.Outer.UserName,
//            Date = ti.Outer.Date,
//            ClientName = ti.Outer.ClientName,
//            BrowserInfo = ti.Outer.BrowserInfo,
//            FullName = ti.Inner.FirstName + " " + ti.Inner.LastName
//        })
//        .ToList();
//        return records;
//    }
//    public List<UserLoginAttemptDto> UserLoginAttempt(string? datePersian)
//    {

//        var dateMiladi = DateManagment.ShamsiToMiladiWithFullDate(datePersian);
//        var addDay = datePersian is null ? 7 : 0;
//        var records = _db.UserLoginAttempt
//        .Join(
//            inner: _db.User,
//            outerKeySelector: u => u.UserId,
//            innerKeySelector: u0 => (long?)u0.Id,
//            resultSelector: (u, u0) => new { Outer = u, Inner = u0 }
//        )
//        .Where(ti => ti.Outer.UserId == _mySession.UserId && ti.Outer.Date.AddDays(addDay).Date <= dateMiladi.Date)
//        .OrderByDescending(ti => ti.Outer.Date)
//        .Take(10)
//        .Select(ti => new UserLoginAttemptDto
//        {
//            ClientIpAddress = ti.Outer.ClientIpAddress,
//            IsLogin = ti.Outer.IsLogin,
//            UserId = ti.Outer.UserId,
//            UserName = ti.Outer.UserName,
//            Date = ti.Outer.Date,
//            ClientName = ti.Outer.ClientName,
//            BrowserInfo = ti.Outer.BrowserInfo,
//            FullName = ti.Inner.FirstName + " " + ti.Inner.LastName
//        })
//        .ToList();
//        return records;
//    }
//    public List<UserPermissionDto> GetPermissionUser()
//    {
//        return
//        _generalServices.GetPermissionUser();
//    }

//    //public IResult VerifyPhoneNumber(string phoneNumber)
//    //{

//    //    var user = _db.User.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
//    //    if (user == null)
//    //    {
//    //        _result.Rsl = false;
//    //        _result.Message = AllMessage.PhoneNumberNotExist;
//    //        return _result;

//    //    }
//    //    SmsVerifyDtoes smsVerifyDtoes = new SmsVerifyDtoes();
//    //    smsVerifyDtoes.User = _mapper.Map<UserDto>(user);
//    //    smsVerifyDtoes.SendDate = DateTime.Now;
//    //    smsVerifyDtoes.SmsCode = Tools.GenerateSixDigit();
//    //    smsVerifyDtoes.UserId = UserId;
//    //    var test = _melipayamakService.SendSms(smsVerifyDtoes);
//    //    _result.Rsl = true;
//    //    _result.Message = "کد ارسالی به شماره همراه خود را وارد کنید";
//    //    return _result;
//    //}


//    public IResult SendCodeNumber(string phoneNumber, bool phonNumberExist, SmsTyp typ)
//    {

//        if (string.IsNullOrEmpty(phoneNumber))
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.EnterPhoneNumber;
//            return _result;
//        }

//        if (phoneNumber.StartsWith("0"))
//            phoneNumber = phoneNumber[1..];
//        if (phoneNumber.StartsWith("+"))
//            phoneNumber = phoneNumber[1..];
//        if (phoneNumber.StartsWith("98"))
//            phoneNumber = phoneNumber[2..];
//        phoneNumber = "0" + phoneNumber;
//        if (!phoneNumber.StartsWith("09") || phoneNumber.Length != 11)
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.PhoneNumberIsNotCorrect;
//            return _result;
//        }


//        var isUser = _db.User.Any(x => x.PhoneNumber == phoneNumber);
//        if (isUser && !phonNumberExist)
//        {
//            _result.Rsl = false;
//            _result.Data = "UserIsExist";
//            _result.Message = AllMessage.PhoneNumberIsregister;
//            return _result;
//        }
//        if (!isUser && phonNumberExist)
//        {
//            _result.Rsl = false;
//            _result.Data = "UserIsExist";
//            _result.Message = AllMessage.PhoneNumberNotExist;
//            return _result;
//        }

//        var resualt = _payamakService.SendSmsCode(phoneNumber, typ);

//        return resualt;
//    }
//    public IResult ValidateCode(string phoneNumber, string code, SmsTyp typ)
//    {
//        var resualt = _payamakService.VerifyCode(phoneNumber, code, typ);
//        if (resualt.Rsl)
//        {
//            _mySession.SetSession("PhoneNumberValidate", phoneNumber);
//            var user = _db.User.Any(x => x.PhoneNumber == phoneNumber);
//            if (!user)
//            {
//                resualt.Data = false;
//            }
//            else
//            {
//                resualt.Data = true;
//                resualt.Message = AllMessage.PhoneNumberIsregister;

//            }
//        }
//        return resualt;
//    }
//    public string GetPhoneNumberValidate()
//    {
//        return _mySession.GetSession("PhoneNumberValidate");
//    }
//    public IResult ResetPass(string pass, string repass)
//    {
//        if (pass.Length < 6)
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.PassMinLength;
//            return _result;
//        }
//        if (pass != repass)
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.PassNotEqual;
//            return _result;
//        }
//        var hashPass = PasswordManagment.HashingPassword(pass);

//        var user = _db.User.FirstOrDefault(x => x.PhoneNumber == GetPhoneNumberValidate());
//        if (user == null)
//        {
//            _result.Rsl = false;
//            _result.Message = AllMessage.PhoneNumberNotExist;
//            return _result;
//        }

//        user.Password = hashPass.Password;
//        user.SaltPassword = hashPass.SaltPassword;
//        _db.SaveChanges();
//        _result.Rsl = true;
//        _result.Message = AllMessage.SaveSuccess;
//        return _result;
//    }
//}
