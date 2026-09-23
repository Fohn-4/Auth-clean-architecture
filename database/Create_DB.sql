CREATE DATABASE auth_schema CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE auth_schema;

CREATE TABLE roles (
    id   TINYINT     NOT NULL,
    name VARCHAR(30) NOT NULL,
    PRIMARY KEY (id),
    UNIQUE KEY uq_roles_name (name)
);

CREATE TABLE images (
    id            CHAR(32)     NOT NULL,
    storage_key   VARCHAR(500) NOT NULL,
    mime_type     VARCHAR(100) NOT NULL,
    original_name VARCHAR(255) NULL,
    created_at    DATETIME     NOT NULL DEFAULT (UTC_TIMESTAMP()),
    PRIMARY KEY (id)
);

CREATE TABLE addresses (
    id          CHAR(32)     NOT NULL,
    street      VARCHAR(200) NOT NULL,
    number      VARCHAR(20)  NOT NULL,
    box         VARCHAR(20)  NULL,
    postal_code VARCHAR(20)  NOT NULL,
    city        VARCHAR(100) NOT NULL,
    country     CHAR(2)      NOT NULL,
    created_at  DATETIME     NOT NULL DEFAULT (UTC_TIMESTAMP()),
    PRIMARY KEY (id)
);

CREATE TABLE users (
    id              CHAR(32)     NOT NULL,
    email           VARCHAR(320) NOT NULL,
    hashed_password VARCHAR(255) NULL,
    first_name      VARCHAR(100) NULL,
    last_name       VARCHAR(100) NULL,
    phone_number    VARCHAR(30)  NULL,
    address_id      CHAR(32)     NULL,
    avatar_image_id CHAR(32)     NULL,
    role_id         TINYINT      NOT NULL,
    email_verified_at     DATETIME NULL,
    last_login_at         DATETIME NULL,
    failed_login_attempts INT      NOT NULL DEFAULT 0,
    locked_until          DATETIME NULL,
    created_at DATETIME NOT NULL DEFAULT (UTC_TIMESTAMP()),
    updated_at DATETIME NULL,
    deleted_at DATETIME NULL,
    PRIMARY KEY (id),
    UNIQUE KEY uq_users_email (email),
    CONSTRAINT fk_users_role    FOREIGN KEY (role_id)         REFERENCES roles(id),
    CONSTRAINT fk_users_address FOREIGN KEY (address_id)      REFERENCES addresses(id),
    CONSTRAINT fk_users_avatar  FOREIGN KEY (avatar_image_id) REFERENCES images(id) ON DELETE SET NULL
);

CREATE TABLE user_tokens (
    id         CHAR(32)    NOT NULL,
    user_id    CHAR(32)    NOT NULL,
    type       VARCHAR(30) NOT NULL,
    token_hash CHAR(64)    NOT NULL,
    expires_at DATETIME    NOT NULL,
    used_at    DATETIME    NULL,
    created_at DATETIME    NOT NULL DEFAULT (UTC_TIMESTAMP()),
    PRIMARY KEY (id),
    UNIQUE KEY uq_user_tokens_hash (token_hash),
    CONSTRAINT fk_user_tokens_user FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE refresh_tokens (
    id         CHAR(32)  NOT NULL,
    user_id    CHAR(32)  NOT NULL,
    token_hash CHAR(64)  NOT NULL,
    expires_at DATETIME  NOT NULL,
    revoked_at DATETIME  NULL,
    created_at DATETIME  NOT NULL DEFAULT (UTC_TIMESTAMP()),
    PRIMARY KEY (id),
    UNIQUE KEY uq_refresh_tokens_hash (token_hash),
    CONSTRAINT fk_refresh_tokens_user FOREIGN KEY (user_id) REFERENCES users(id)
);

