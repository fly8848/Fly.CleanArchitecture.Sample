CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory`
(
    `MigrationId`
    varchar
(
    150
) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar
(
    32
) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY
(
    `MigrationId`
)
    ) CHARACTER SET =utf8mb4;

START TRANSACTION;

ALTER
DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Order`
(
    `Id`              int                                NOT NULL AUTO_INCREMENT,
    `CustomerName`    varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CustomerOrderNo` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `OrderNo`         varchar(255) CHARACTER SET utf8mb4 NULL,
    `Remark`          varchar(255) CHARACTER SET utf8mb4 NULL,
    `CreatedTime`     datetime(6) NOT NULL,
    `CreatedBy`       varchar(255) CHARACTER SET utf8mb4 NULL,
    `IsDeleted`       tinyint(1) NOT NULL,
    `DeletedTime`     datetime(6) NULL,
    `DeletedBy`       varchar(255) CHARACTER SET utf8mb4 NULL,
    `UpdatedTime`     datetime(6) NULL,
    `UpdatedBy`       varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Order` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderDetail`
(
    `Id`             int                                NOT NULL AUTO_INCREMENT,
    `OrderId`        int                                NOT NULL,
    `Name`           varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Qty`            int                                NOT NULL,
    `Money_Amount`   decimal(18, 6)                     NOT NULL,
    `Money_Currency` int                                NOT NULL,
    CONSTRAINT `PK_OrderDetail` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250624150928_Init', '8.0.17');

COMMIT;

