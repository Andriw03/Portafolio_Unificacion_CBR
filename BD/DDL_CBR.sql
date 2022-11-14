-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema UNIONLINE
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema UNIONLINE
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `UNIONLINE` DEFAULT CHARACTER SET utf8 ;
USE `UNIONLINE` ;

-- -----------------------------------------------------
-- Table `UNIONLINE`.`B_PAGO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`B_PAGO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`B_PAGO` (
  `id_boleta` INT NOT NULL AUTO_INCREMENT,
  `fecha_emision` DATETIME NOT NULL,
  `monto_pago` INT NOT NULL,
  `tipo_pago` INT NOT NULL,
  PRIMARY KEY (`id_boleta`))
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`CLAS_PROP`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`CLAS_PROP` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`CLAS_PROP` (
  `id_clas` INT NOT NULL AUTO_INCREMENT,
  `foja` INT NOT NULL,
  `numero` INT NOT NULL,
  `anno` DATETIME NOT NULL,
  `razon_social` VARCHAR(45) NULL DEFAULT NULL,
  `rut_empresa` VARCHAR(15) NULL DEFAULT NULL,
  PRIMARY KEY (`id_clas`))
ENGINE = InnoDB
AUTO_INCREMENT = 18
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`REGION`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`REGION` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`REGION` (
  `id_region` INT NOT NULL,
  `nombre_region` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_region`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`PROVINCIA`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`PROVINCIA` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`PROVINCIA` (
  `id_provincia` INT NOT NULL,
  `nombre_provincia` VARCHAR(45) NOT NULL,
  `REGION_id_region` INT NOT NULL,
  PRIMARY KEY (`id_provincia`, `REGION_id_region`),
  INDEX `fk_PROVINCIA_REGION1_idx` (`REGION_id_region` ASC) VISIBLE,
  CONSTRAINT `fk_PROVINCIA_REGION1`
    FOREIGN KEY (`REGION_id_region`)
    REFERENCES `UNIONLINE`.`REGION` (`id_region`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`COMUNA`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`COMUNA` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`COMUNA` (
  `id_comuna` INT NOT NULL,
  `nombre_comuna` VARCHAR(45) NOT NULL,
  `PROVINCIA_id_provincia` INT NOT NULL,
  PRIMARY KEY (`id_comuna`, `PROVINCIA_id_provincia`),
  INDEX `fk_COMUNA_PROVINCIA1_idx` (`PROVINCIA_id_provincia` ASC) VISIBLE,
  CONSTRAINT `fk_COMUNA_PROVINCIA1`
    FOREIGN KEY (`PROVINCIA_id_provincia`)
    REFERENCES `UNIONLINE`.`PROVINCIA` (`id_provincia`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`DIRECCION`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`DIRECCION` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`DIRECCION` (
  `id_direccion` INT NOT NULL AUTO_INCREMENT,
  `nombre_calle` VARCHAR(45) NOT NULL,
  `numero_casa` INT NOT NULL,
  `COMUNA_id_comuna` INT NOT NULL,
  PRIMARY KEY (`id_direccion`, `COMUNA_id_comuna`),
  INDEX `fk_DIRECCION_COMUNA1_idx` (`COMUNA_id_comuna` ASC) VISIBLE,
  CONSTRAINT `fk_DIRECCION_COMUNA1`
    FOREIGN KEY (`COMUNA_id_comuna`)
    REFERENCES `UNIONLINE`.`COMUNA` (`id_comuna`))
ENGINE = InnoDB
AUTO_INCREMENT = 30
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`DUENNO_PROP`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`DUENNO_PROP` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`DUENNO_PROP` (
  `id_duenno` INT NOT NULL AUTO_INCREMENT,
  `rut_duenno` VARCHAR(15) NOT NULL,
  `primer_nombre` VARCHAR(45) NOT NULL,
  `segundo_nombre` VARCHAR(45) NOT NULL,
  `primer_apellido` VARCHAR(45) NOT NULL,
  `segundo_apellido` VARCHAR(45) NOT NULL,
  `correo_electronico` VARCHAR(50) NOT NULL,
  `telefono` VARCHAR(12) NOT NULL,
  `copia_carnet` LONGBLOB NULL DEFAULT NULL,
  PRIMARY KEY (`id_duenno`))
ENGINE = InnoDB
AUTO_INCREMENT = 14
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`TIPO_PROPIEDAD`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`TIPO_PROPIEDAD` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`TIPO_PROPIEDAD` (
  `id_tipoP` INT NOT NULL,
  `nombre_tipoP` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_tipoP`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`PROPIEDAD`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`PROPIEDAD` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`PROPIEDAD` (
  `id_propiedad` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(100) NOT NULL,
  `escritura` LONGBLOB NOT NULL,
  `DIRECCION_id_direccion` INT NOT NULL,
  `TIPO_PROPIEDAD_id_tipoP` INT NOT NULL,
  `CLAS_PROP_id_clas` INT NOT NULL,
  `DUENNO_PROP_id_duenno` INT NOT NULL,
  PRIMARY KEY (`id_propiedad`, `DIRECCION_id_direccion`, `TIPO_PROPIEDAD_id_tipoP`, `CLAS_PROP_id_clas`, `DUENNO_PROP_id_duenno`),
  INDEX `fk_PROPIEDAD_DIRECCION1_idx` (`DIRECCION_id_direccion` ASC) VISIBLE,
  INDEX `fk_PROPIEDAD_TIPO_PROPIEDAD1_idx` (`TIPO_PROPIEDAD_id_tipoP` ASC) VISIBLE,
  INDEX `fk_PROPIEDAD_CLAS_PROP1_idx` (`CLAS_PROP_id_clas` ASC) VISIBLE,
  INDEX `fk_PROPIEDAD_DUENNO_PROP1_idx` (`DUENNO_PROP_id_duenno` ASC) VISIBLE,
  CONSTRAINT `fk_PROPIEDAD_CLAS_PROP1`
    FOREIGN KEY (`CLAS_PROP_id_clas`)
    REFERENCES `UNIONLINE`.`CLAS_PROP` (`id_clas`),
  CONSTRAINT `fk_PROPIEDAD_DIRECCION1`
    FOREIGN KEY (`DIRECCION_id_direccion`)
    REFERENCES `UNIONLINE`.`DIRECCION` (`id_direccion`),
  CONSTRAINT `fk_PROPIEDAD_DUENNO_PROP1`
    FOREIGN KEY (`DUENNO_PROP_id_duenno`)
    REFERENCES `UNIONLINE`.`DUENNO_PROP` (`id_duenno`),
  CONSTRAINT `fk_PROPIEDAD_TIPO_PROPIEDAD1`
    FOREIGN KEY (`TIPO_PROPIEDAD_id_tipoP`)
    REFERENCES `UNIONLINE`.`TIPO_PROPIEDAD` (`id_tipoP`))
ENGINE = InnoDB
AUTO_INCREMENT = 15
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`T_TRAMITE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`T_TRAMITE` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`T_TRAMITE` (
  `id_tipoT` INT NOT NULL AUTO_INCREMENT,
  `nombre_tipoT` VARCHAR(65) NOT NULL,
  `TIPO_PROPIEDAD_id_tipoP` INT NOT NULL,
  PRIMARY KEY (`id_tipoT`, `TIPO_PROPIEDAD_id_tipoP`),
  INDEX `fk_T_TRAMITE_TIPO_PROPIEDAD1_idx` (`TIPO_PROPIEDAD_id_tipoP` ASC) VISIBLE,
  CONSTRAINT `fk_T_TRAMITE_TIPO_PROPIEDAD1`
    FOREIGN KEY (`TIPO_PROPIEDAD_id_tipoP`)
    REFERENCES `UNIONLINE`.`TIPO_PROPIEDAD` (`id_tipoP`))
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`TRAMITE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`TRAMITE` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`TRAMITE` (
  `id_tramite` INT NOT NULL AUTO_INCREMENT,
  `nombre_tramite` VARCHAR(250) NOT NULL,
  `valor_tramite` VARCHAR(45) NOT NULL,
  `estado` VARCHAR(45) NOT NULL,
  `descripcion` VARCHAR(250) NOT NULL,
  `T_TRAMITE_id_tipoT` INT NOT NULL,
  `t_documento` VARCHAR(250) NULL DEFAULT 'No Aplica',
  PRIMARY KEY (`id_tramite`, `T_TRAMITE_id_tipoT`),
  INDEX `fk_TRAMITE1_T_TRAMITE1_idx` (`T_TRAMITE_id_tipoT` ASC) VISIBLE,
  CONSTRAINT `fk_TRAMITE1_T_TRAMITE1`
    FOREIGN KEY (`T_TRAMITE_id_tipoT`)
    REFERENCES `UNIONLINE`.`T_TRAMITE` (`id_tipoT`))
ENGINE = InnoDB
AUTO_INCREMENT = 15
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`HOR_ATENCION`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`HOR_ATENCION` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`HOR_ATENCION` (
  `id_horario` INT NOT NULL AUTO_INCREMENT,
  `dias_atencion` VARCHAR(45) NOT NULL,
  `horario_apertura` VARCHAR(10) NOT NULL,
  `horario_cierre` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`id_horario`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`CBR`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`CBR` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`CBR` (
  `id_cbr` INT NOT NULL AUTO_INCREMENT,
  `nombre_cbr` VARCHAR(100) NOT NULL,
  `correo_cbr` VARCHAR(50) NOT NULL,
  `telefono` VARCHAR(12) NOT NULL,
  `HOR_ATENCION_id_horario` INT NOT NULL,
  `DIRECCION_id_direccion` INT NOT NULL,
  PRIMARY KEY (`id_cbr`, `HOR_ATENCION_id_horario`, `DIRECCION_id_direccion`),
  INDEX `fk_CBR_HOR_ATENCION1_idx` (`HOR_ATENCION_id_horario` ASC) VISIBLE,
  INDEX `fk_CBR_DIRECCION1_idx` (`DIRECCION_id_direccion` ASC) VISIBLE,
  CONSTRAINT `fk_CBR_DIRECCION1`
    FOREIGN KEY (`DIRECCION_id_direccion`)
    REFERENCES `UNIONLINE`.`DIRECCION` (`id_direccion`),
  CONSTRAINT `fk_CBR_HOR_ATENCION1`
    FOREIGN KEY (`HOR_ATENCION_id_horario`)
    REFERENCES `UNIONLINE`.`HOR_ATENCION` (`id_horario`))
ENGINE = InnoDB
AUTO_INCREMENT = 8
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`T_USUARIO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`T_USUARIO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`T_USUARIO` (
  `id_tipoU` INT NOT NULL,
  `nombre_tipoU` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_tipoU`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`USUARIO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`USUARIO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`USUARIO` (
  `id_usuario` INT NOT NULL AUTO_INCREMENT,
  `rut_usuario` VARCHAR(15) NOT NULL,
  `contrasenna` VARCHAR(100) NOT NULL,
  `primer_nombre` VARCHAR(45) NOT NULL,
  `segundo_nombre` VARCHAR(45) NULL DEFAULT NULL,
  `primer_apellido` VARCHAR(45) NOT NULL,
  `segundo_apellido` VARCHAR(45) NOT NULL,
  `correo_electronico` VARCHAR(45) NOT NULL,
  `telefono` VARCHAR(12) NOT NULL,
  `CBR_id_cbr` INT NOT NULL,
  `T_USUARIO_id_tipoU` INT NOT NULL,
  PRIMARY KEY (`id_usuario`, `T_USUARIO_id_tipoU`),
  INDEX `fk_USUARIO_CBR1_idx` (`CBR_id_cbr` ASC) VISIBLE,
  INDEX `fk_USUARIO_T_USUARIO1_idx` (`T_USUARIO_id_tipoU` ASC) VISIBLE,
  CONSTRAINT `fk_USUARIO_CBR1`
    FOREIGN KEY (`CBR_id_cbr`)
    REFERENCES `UNIONLINE`.`CBR` (`id_cbr`),
  CONSTRAINT `fk_USUARIO_T_USUARIO1`
    FOREIGN KEY (`T_USUARIO_id_tipoU`)
    REFERENCES `UNIONLINE`.`T_USUARIO` (`id_tipoU`))
ENGINE = InnoDB
AUTO_INCREMENT = 62
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`SOLICITUD`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`SOLICITUD` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`SOLICITUD` (
  `id_soli` INT NOT NULL AUTO_INCREMENT,
  `fecha_solicitud` DATETIME NOT NULL,
  `fecha_cierre` DATETIME NULL DEFAULT NULL,
  `estado` VARCHAR(45) NOT NULL,
  `numero_seguimiento` VARCHAR(45) NOT NULL,
  `Comentario` VARCHAR(100) NULL DEFAULT NULL,
  `USUARIO_id_usuario` INT NOT NULL,
  `PROPIEDAD_id_propiedad` INT NOT NULL,
  `TRAMITE_id_tramite` INT NOT NULL,
  PRIMARY KEY (`id_soli`, `USUARIO_id_usuario`, `PROPIEDAD_id_propiedad`, `TRAMITE_id_tramite`),
  INDEX `fk_SOLICITUD_USUARIO_idx` (`USUARIO_id_usuario` ASC) VISIBLE,
  INDEX `fk_SOLICITUD_PROPIEDAD1_idx` (`PROPIEDAD_id_propiedad` ASC) VISIBLE,
  INDEX `fk_SOLICITUD_TRAMITE1_idx` (`TRAMITE_id_tramite` ASC) VISIBLE,
  CONSTRAINT `fk_SOLICITUD_PROPIEDAD1`
    FOREIGN KEY (`PROPIEDAD_id_propiedad`)
    REFERENCES `UNIONLINE`.`PROPIEDAD` (`id_propiedad`),
  CONSTRAINT `fk_SOLICITUD_TRAMITE1`
    FOREIGN KEY (`TRAMITE_id_tramite`)
    REFERENCES `UNIONLINE`.`TRAMITE` (`id_tramite`),
  CONSTRAINT `fk_SOLICITUD_USUARIO`
    FOREIGN KEY (`USUARIO_id_usuario`)
    REFERENCES `UNIONLINE`.`USUARIO` (`id_usuario`))
ENGINE = InnoDB
AUTO_INCREMENT = 30
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`CAR_COMPRA`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`CAR_COMPRA` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`CAR_COMPRA` (
  `id_carrito` INT NOT NULL AUTO_INCREMENT,
  `estado` INT NOT NULL DEFAULT '0',
  `SOLICITUD_id_soli` INT NOT NULL,
  PRIMARY KEY (`id_carrito`, `SOLICITUD_id_soli`),
  INDEX `fk_CAR_COMPRA_SOLICITUD1_idx` (`SOLICITUD_id_soli` ASC) VISIBLE,
  CONSTRAINT `fk_CAR_COMPRA_SOLICITUD1`
    FOREIGN KEY (`SOLICITUD_id_soli`)
    REFERENCES `UNIONLINE`.`SOLICITUD` (`id_soli`))
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`TIPO_DOCUMENTO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`TIPO_DOCUMENTO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`TIPO_DOCUMENTO` (
  `id_tipodoc` INT NOT NULL,
  `tipo_doc` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_tipodoc`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`DOCUMENTO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`DOCUMENTO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`DOCUMENTO` (
  `id_documento` INT NOT NULL AUTO_INCREMENT,
  `nombre_doc` VARCHAR(45) NOT NULL,
  `doc` LONGBLOB NULL DEFAULT NULL,
  `SOLICITUD_id_soli` INT NOT NULL,
  `TIPO_DOCUMENTO_id_tipodoc` INT NOT NULL,
  PRIMARY KEY (`id_documento`, `SOLICITUD_id_soli`, `TIPO_DOCUMENTO_id_tipodoc`),
  INDEX `fk_DOCUMENTO_SOLICITUD1_idx` (`SOLICITUD_id_soli` ASC) VISIBLE,
  INDEX `fk_DOCUMENTO_TIPO_DOCUMENTO1_idx` (`TIPO_DOCUMENTO_id_tipodoc` ASC) VISIBLE,
  CONSTRAINT `fk_DOCUMENTO_SOLICITUD1`
    FOREIGN KEY (`SOLICITUD_id_soli`)
    REFERENCES `UNIONLINE`.`SOLICITUD` (`id_soli`),
  CONSTRAINT `fk_DOCUMENTO_TIPO_DOCUMENTO1`
    FOREIGN KEY (`TIPO_DOCUMENTO_id_tipodoc`)
    REFERENCES `UNIONLINE`.`TIPO_DOCUMENTO` (`id_tipodoc`))
ENGINE = InnoDB
AUTO_INCREMENT = 17
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`ERROR`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`ERROR` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`ERROR` (
  `id_error` INT NOT NULL AUTO_INCREMENT,
  `codigo_error` VARCHAR(45) NOT NULL,
  `mensaje_error` VARCHAR(100) NOT NULL,
  `fecha_error` DATETIME NOT NULL,
  PRIMARY KEY (`id_error`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`TIPO_PAGO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`TIPO_PAGO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`TIPO_PAGO` (
  `id_tipoP` INT NOT NULL,
  `nombre_tipoP` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_tipoP`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`ESTADO_PAGO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`ESTADO_PAGO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`ESTADO_PAGO` (
  `id_estado` INT NOT NULL AUTO_INCREMENT,
  `CAR_COMPRA_id_carrito` INT NOT NULL,
  `TIPO_PAGO_id_tipoP` INT NOT NULL,
  PRIMARY KEY (`id_estado`, `CAR_COMPRA_id_carrito`, `TIPO_PAGO_id_tipoP`),
  INDEX `fk_ESTADO_PAGO_CAR_COMPRA1_idx` (`CAR_COMPRA_id_carrito` ASC) VISIBLE,
  INDEX `fk_ESTADO_PAGO_TIPO_PAGO1_idx` (`TIPO_PAGO_id_tipoP` ASC) VISIBLE,
  CONSTRAINT `fk_ESTADO_PAGO_CAR_COMPRA1`
    FOREIGN KEY (`CAR_COMPRA_id_carrito`)
    REFERENCES `UNIONLINE`.`CAR_COMPRA` (`id_carrito`),
  CONSTRAINT `fk_ESTADO_PAGO_TIPO_PAGO1`
    FOREIGN KEY (`TIPO_PAGO_id_tipoP`)
    REFERENCES `UNIONLINE`.`TIPO_PAGO` (`id_tipoP`))
ENGINE = InnoDB
AUTO_INCREMENT = 230
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`FORMULARIO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`FORMULARIO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`FORMULARIO` (
  `id_formulario` INT NOT NULL AUTO_INCREMENT,
  `nombre_form` VARCHAR(45) NOT NULL,
  `telefono` VARCHAR(45) NOT NULL,
  `correo_form` VARCHAR(45) NOT NULL,
  `asunto_form` VARCHAR(45) NOT NULL,
  `detalle_form` VARCHAR(500) NOT NULL,
  `estado` VARCHAR(45) NULL DEFAULT NULL,
  `USUARIO_id_usuario` INT NOT NULL,
  PRIMARY KEY (`id_formulario`),
  INDEX `fk_FORMULARIO_USUARIO1_idx` (`USUARIO_id_usuario` ASC) VISIBLE,
  CONSTRAINT `fk_FORMULARIO_USUARIO1`
    FOREIGN KEY (`USUARIO_id_usuario`)
    REFERENCES `UNIONLINE`.`USUARIO` (`id_usuario`))
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`auth_group`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`auth_group` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`auth_group` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(150) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `name` (`name` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`django_content_type`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`django_content_type` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`django_content_type` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `app_label` VARCHAR(100) NOT NULL,
  `model` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `django_content_type_app_label_model_76bd3d3b_uniq` (`app_label` ASC, `model` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 42
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`auth_permission`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`auth_permission` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`auth_permission` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `content_type_id` INT NOT NULL,
  `codename` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `auth_permission_content_type_id_codename_01ab375a_uniq` (`content_type_id` ASC, `codename` ASC) VISIBLE,
  CONSTRAINT `auth_permission_content_type_id_2f476e4b_fk_django_co`
    FOREIGN KEY (`content_type_id`)
    REFERENCES `UNIONLINE`.`django_content_type` (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 165
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`auth_group_permissions`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`auth_group_permissions` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`auth_group_permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `group_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `auth_group_permissions_group_id_permission_id_0cd325b0_uniq` (`group_id` ASC, `permission_id` ASC) VISIBLE,
  INDEX `auth_group_permissio_permission_id_84c5c92e_fk_auth_perm` (`permission_id` ASC) VISIBLE,
  CONSTRAINT `auth_group_permissio_permission_id_84c5c92e_fk_auth_perm`
    FOREIGN KEY (`permission_id`)
    REFERENCES `UNIONLINE`.`auth_permission` (`id`),
  CONSTRAINT `auth_group_permissions_group_id_b120cbf9_fk_auth_group_id`
    FOREIGN KEY (`group_id`)
    REFERENCES `UNIONLINE`.`auth_group` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`auth_user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`auth_user` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`auth_user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `password` VARCHAR(128) NOT NULL,
  `last_login` DATETIME(6) NULL DEFAULT NULL,
  `is_superuser` TINYINT(1) NOT NULL,
  `username` VARCHAR(150) NOT NULL,
  `first_name` VARCHAR(150) NOT NULL,
  `last_name` VARCHAR(150) NOT NULL,
  `email` VARCHAR(254) NOT NULL,
  `is_staff` TINYINT(1) NOT NULL,
  `is_active` TINYINT(1) NOT NULL,
  `date_joined` DATETIME(6) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `username` (`username` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 15
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`auth_user_groups`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`auth_user_groups` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`auth_user_groups` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `group_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `auth_user_groups_user_id_group_id_94350c0c_uniq` (`user_id` ASC, `group_id` ASC) VISIBLE,
  INDEX `auth_user_groups_group_id_97559544_fk_auth_group_id` (`group_id` ASC) VISIBLE,
  CONSTRAINT `auth_user_groups_group_id_97559544_fk_auth_group_id`
    FOREIGN KEY (`group_id`)
    REFERENCES `UNIONLINE`.`auth_group` (`id`),
  CONSTRAINT `auth_user_groups_user_id_6a12ed8b_fk_auth_user_id`
    FOREIGN KEY (`user_id`)
    REFERENCES `UNIONLINE`.`auth_user` (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`auth_user_user_permissions`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`auth_user_user_permissions` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`auth_user_user_permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `auth_user_user_permissions_user_id_permission_id_14a6b632_uniq` (`user_id` ASC, `permission_id` ASC) VISIBLE,
  INDEX `auth_user_user_permi_permission_id_1fbb5f2c_fk_auth_perm` (`permission_id` ASC) VISIBLE,
  CONSTRAINT `auth_user_user_permi_permission_id_1fbb5f2c_fk_auth_perm`
    FOREIGN KEY (`permission_id`)
    REFERENCES `UNIONLINE`.`auth_permission` (`id`),
  CONSTRAINT `auth_user_user_permissions_user_id_a95ead1b_fk_auth_user_id`
    FOREIGN KEY (`user_id`)
    REFERENCES `UNIONLINE`.`auth_user` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`django_admin_log`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`django_admin_log` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`django_admin_log` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `action_time` DATETIME(6) NOT NULL,
  `object_id` LONGTEXT NULL DEFAULT NULL,
  `object_repr` VARCHAR(200) NOT NULL,
  `action_flag` SMALLINT UNSIGNED NOT NULL,
  `change_message` LONGTEXT NOT NULL,
  `content_type_id` INT NULL DEFAULT NULL,
  `user_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `django_admin_log_content_type_id_c4bce8eb_fk_django_co` (`content_type_id` ASC) VISIBLE,
  INDEX `django_admin_log_user_id_c564eba6_fk_auth_user_id` (`user_id` ASC) VISIBLE,
  CONSTRAINT `django_admin_log_content_type_id_c4bce8eb_fk_django_co`
    FOREIGN KEY (`content_type_id`)
    REFERENCES `UNIONLINE`.`django_content_type` (`id`),
  CONSTRAINT `django_admin_log_user_id_c564eba6_fk_auth_user_id`
    FOREIGN KEY (`user_id`)
    REFERENCES `UNIONLINE`.`auth_user` (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 22
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`django_migrations`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`django_migrations` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`django_migrations` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `app` VARCHAR(255) NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `applied` DATETIME(6) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 21
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`django_session`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`django_session` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`django_session` (
  `session_key` VARCHAR(40) NOT NULL,
  `session_data` LONGTEXT NOT NULL,
  `expire_date` DATETIME(6) NOT NULL,
  PRIMARY KEY (`session_key`),
  INDEX `django_session_expire_date_a5c62663` (`expire_date` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;

USE `UNIONLINE` ;

-- -----------------------------------------------------
-- function FN_CALCULAR_MONTO
-- -----------------------------------------------------

USE `UNIONLINE`;
DROP function IF EXISTS `UNIONLINE`.`FN_CALCULAR_MONTO`;

DELIMITER $$
USE `UNIONLINE`$$
CREATE DEFINER=`root`@`%` FUNCTION `FN_CALCULAR_MONTO`(estado int) RETURNS int
BEGIN
	declare monto_total int;

SELECT 
    SUM(TRAMITE.valor_tramite)
INTO monto_total FROM
    UNIONLINE.ESTADO_PAGO
        INNER JOIN
    UNIONLINE.CAR_COMPRA ON ESTADO_PAGO.CAR_COMPRA_id_carrito = CAR_COMPRA.id_carrito
        INNER JOIN
    UNIONLINE.SOLICITUD ON CAR_COMPRA.SOLICITUD_id_soli = SOLICITUD.id_soli
        INNER JOIN
    UNIONLINE.TRAMITE ON SOLICITUD.TRAMITE_id_tramite = TRAMITE.id_tramite
WHERE
    ESTADO_PAGO.id_estado = estado;

IF monto_total > 0 THEN
	
    RETURN monto_total;
    
ELSE 
	INSERT INTO  `UNIONLINE`. `ERROR` (`codigo_error`,`mensaje_error`,`fecha_error`)
    VALUES
    ('1002','Carrito de compra sin monto',now());
    RETURN 0;
END IF;
END$$

DELIMITER ;
USE `UNIONLINE`;

DELIMITER $$

USE `UNIONLINE`$$
DROP TRIGGER IF EXISTS `UNIONLINE`.`ESTADO_PAGO_AFTER_INSERT` $$
USE `UNIONLINE`$$
CREATE DEFINER = CURRENT_USER TRIGGER `UNIONLINE`.`ESTADO_PAGO_AFTER_INSERT` AFTER INSERT ON `ESTADO_PAGO` FOR EACH ROW
BEGIN
declare var_monto INTEGER; 
declare tipoP varchar(50);
SELECT nombre_tipoP into tipoP FROM UNIONLINE.ESTADO_PAGO inner join UNIONLINE.TIPO_PAGO on UNIONLINE.ESTADO_PAGO.TIPO_PAGO_id_tipoP = UNIONLINE.TIPO_PAGO.id_tipoP where id_estado = NEW.id_estado;
set var_monto = FN_AGREGAR_BOLETA (NEW.id_estado);
INSERT INTO  `UNIONLINE`. `B_PAGO` (`fecha_emision`,`monto_pago`,`tipo_pago`) VALUES (now(),monto_total,tipoP);
UPDATE `UNIONLINE`.`CAR_COMPRA` SET `estado` = 1 WHERE `id_carrito` = NEW.CAR_COMPRA_id_carrito;
END$$


DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
