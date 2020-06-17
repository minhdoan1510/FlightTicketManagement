# API Reference

### 1. Tài khoản
#### a. Lấy thông tin Access Token 
Phương thức và URL: POST /api/account/login

Body:
```sh
{
    "Username": "<username>",
    "Password": "<password>"
}
```
Response:
```sh
{
    "isSuccess": true,
    "result": {
        "name": "<name>",
        "token": "<accesstoken>",
        "id": "<iduser>"
    }
}
```
#### a. Đăng kí tài khoản mới
Phương thức và URL: POST /api/account/signup

Body:
```sh
{
    "Username": "<username>",
    "Password": "<password>",
    "Name": "<name>"
}
```
Response:
```sh
{
    "isSuccess": <result>,
    "errorCode": <errorcode>,
    "errorMessenge": "<errormess>"
}
```
