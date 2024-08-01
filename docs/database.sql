CREATE DATABASE IF NOT EXISTS ifredcloud;

CREATE TABLE users (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(60) NOT NULL UNIQUE,
    email VARCHAR(150) NOT NULL,
    password VARCHAR(120) NOT NULL
);

CREATE TABLE users_tokens (
    jwt_token VARCHAR(145) NOT NULL PRIMARY KEY,
    user_id INT NOT NULL,
    expiration DATETIME NULL
)
--Modules