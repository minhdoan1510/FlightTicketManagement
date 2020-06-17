# API Reference

## Cấu trúc chung về API

```sh
{
    "isSuccess": <result>, // Trả về kết quả true|false
    "result": {
        "name": "<name>",
        "token": "<accesstoken>",
        "id": "<iduser>"
    }, // Dữ liệu get được
    "errorCode": <errorcode>, // Mã lỗi
    "errorMessenge": "<errormess>" // Thông tin lỗi
}
```
Các thành phần sẽ thay đổi tuỳ thuộc vào kết quả
## Chức năng
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
    "isSuccess": <result>,
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
