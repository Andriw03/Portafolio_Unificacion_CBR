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
-- Table `UNIONLINE`.`HOR_ATENCION`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`HOR_ATENCION` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`HOR_ATENCION` (
  `id_horario` INT NOT NULL,
  `dias_atencion` VARCHAR(45) NOT NULL,
  `horario_apertura` VARCHAR(10) NOT NULL,
  `horario_cierre` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`id_horario`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`CBR`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`CBR` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`CBR` (
  `id_cbr` INT NOT NULL,
  `nombre_cbr` VARCHAR(100) NOT NULL,
  `correo_cbr` VARCHAR(50) NOT NULL,
  `telefono` VARCHAR(12) NOT NULL,
  `HOR_ATENCION_id_horario` INT NOT NULL,
  PRIMARY KEY (`id_cbr`, `HOR_ATENCION_id_horario`),
  INDEX `fk_CBR_HOR_ATENCION1_idx` (`HOR_ATENCION_id_horario` ASC) VISIBLE,
  CONSTRAINT `fk_CBR_HOR_ATENCION1`
    FOREIGN KEY (`HOR_ATENCION_id_horario`)
    REFERENCES `UNIONLINE`.`HOR_ATENCION` (`id_horario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
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
  `id_usuario` INT NOT NULL,
  `rut_usuario` VARCHAR(15) NOT NULL,
  `contrasenna` VARCHAR(45) NOT NULL,
  `primer_nombre` VARCHAR(45) NOT NULL,
  `segundo_nombre` VARCHAR(45) NOT NULL,
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
    REFERENCES `UNIONLINE`.`CBR` (`id_cbr`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_USUARIO_T_USUARIO1`
    FOREIGN KEY (`T_USUARIO_id_tipoU`)
    REFERENCES `UNIONLINE`.`T_USUARIO` (`id_tipoU`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
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
    REFERENCES `UNIONLINE`.`REGION` (`id_region`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
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
    REFERENCES `UNIONLINE`.`PROVINCIA` (`id_provincia`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`DIRECCION`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`DIRECCION` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`DIRECCION` (
  `id_direccion` INT NOT NULL,
  `nombre_calle` VARCHAR(45) NOT NULL,
  `numero_casa` INT NOT NULL,
  `COMUNA_id_comuna` INT NOT NULL,
  PRIMARY KEY (`id_direccion`, `COMUNA_id_comuna`),
  INDEX `fk_DIRECCION_COMUNA1_idx` (`COMUNA_id_comuna` ASC) VISIBLE,
  CONSTRAINT `fk_DIRECCION_COMUNA1`
    FOREIGN KEY (`COMUNA_id_comuna`)
    REFERENCES `UNIONLINE`.`COMUNA` (`id_comuna`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
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
-- Table `UNIONLINE`.`CLAS_PROP`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`CLAS_PROP` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`CLAS_PROP` (
  `id_clas` INT NOT NULL,
  `foja` INT NOT NULL,
  `numero` INT NOT NULL,
  `anno` DATETIME NOT NULL,
  `razon_social` VARCHAR(45) NULL DEFAULT NULL,
  `rut_empresa` VARCHAR(15) NULL DEFAULT NULL,
  PRIMARY KEY (`id_clas`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`DUENNO_PROP`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`DUENNO_PROP` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`DUENNO_PROP` (
  `id_duenno` INT NOT NULL,
  `rut_duenno` VARCHAR(15) NOT NULL,
  `primer_nombre` VARCHAR(45) NOT NULL,
  `segundo_nombre` VARCHAR(45) NOT NULL,
  `primer_apellido` VARCHAR(45) NOT NULL,
  `segundo_apellido` VARCHAR(45) NOT NULL,
  `correo_electronico` VARCHAR(50) NOT NULL,
  `telefono` VARCHAR(12) NOT NULL,
  PRIMARY KEY (`id_duenno`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`PROPIEDAD`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`PROPIEDAD` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`PROPIEDAD` (
  `id_propiedad` INT NOT NULL,
  `nombre_propiedad` VARCHAR(45) NOT NULL,
  `DIRECCION_id_direccion` INT NOT NULL,
  `TIPO_PROPIEDAD_id_tipoP` INT NOT NULL,
  `CLAS_PROP_id_clas` INT NOT NULL,
  `DUENNO_PROP_id_duenno` INT NOT NULL,
  PRIMARY KEY (`id_propiedad`, `DIRECCION_id_direccion`, `TIPO_PROPIEDAD_id_tipoP`, `CLAS_PROP_id_clas`, `DUENNO_PROP_id_duenno`),
  INDEX `fk_PROPIEDAD_DIRECCION1_idx` (`DIRECCION_id_direccion` ASC) VISIBLE,
  INDEX `fk_PROPIEDAD_TIPO_PROPIEDAD1_idx` (`TIPO_PROPIEDAD_id_tipoP` ASC) VISIBLE,
  INDEX `fk_PROPIEDAD_CLAS_PROP1_idx` (`CLAS_PROP_id_clas` ASC) VISIBLE,
  INDEX `fk_PROPIEDAD_DUENNO_PROP1_idx` (`DUENNO_PROP_id_duenno` ASC) VISIBLE,
  CONSTRAINT `fk_PROPIEDAD_DIRECCION1`
    FOREIGN KEY (`DIRECCION_id_direccion`)
    REFERENCES `UNIONLINE`.`DIRECCION` (`id_direccion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PROPIEDAD_TIPO_PROPIEDAD1`
    FOREIGN KEY (`TIPO_PROPIEDAD_id_tipoP`)
    REFERENCES `UNIONLINE`.`TIPO_PROPIEDAD` (`id_tipoP`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PROPIEDAD_CLAS_PROP1`
    FOREIGN KEY (`CLAS_PROP_id_clas`)
    REFERENCES `UNIONLINE`.`CLAS_PROP` (`id_clas`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PROPIEDAD_DUENNO_PROP1`
    FOREIGN KEY (`DUENNO_PROP_id_duenno`)
    REFERENCES `UNIONLINE`.`DUENNO_PROP` (`id_duenno`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
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
-- Table `UNIONLINE`.`DOCUMUENTO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`DOCUMUENTO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`DOCUMUENTO` (
  `id_documento` INT NOT NULL,
  `nombre_doc` VARCHAR(45) NOT NULL,
  `TIPO_DOCUMENTO_id_tipodoc` INT NOT NULL,
  PRIMARY KEY (`id_documento`, `TIPO_DOCUMENTO_id_tipodoc`),
  INDEX `fk_DOCUMUENTO_TIPO_DOCUMENTO1_idx` (`TIPO_DOCUMENTO_id_tipodoc` ASC) VISIBLE,
  CONSTRAINT `fk_DOCUMUENTO_TIPO_DOCUMENTO1`
    FOREIGN KEY (`TIPO_DOCUMENTO_id_tipodoc`)
    REFERENCES `UNIONLINE`.`TIPO_DOCUMENTO` (`id_tipodoc`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`SOLICITUD`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`SOLICITUD` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`SOLICITUD` (
  `id_soli` INT NOT NULL,
  `fecha_solicitud` DATETIME NOT NULL,
  `fehca_cierre` DATETIME NOT NULL,
  `estado` VARCHAR(45) NOT NULL,
  `USUARIO_id_usuario` INT NOT NULL,
  `PROPIEDAD_id_propiedad` INT NOT NULL,
  `DOCUMUENTO_id_documento` INT NOT NULL,
  PRIMARY KEY (`id_soli`, `USUARIO_id_usuario`, `PROPIEDAD_id_propiedad`),
  INDEX `fk_SOLICITUD_USUARIO_idx` (`USUARIO_id_usuario` ASC) VISIBLE,
  INDEX `fk_SOLICITUD_PROPIEDAD1_idx` (`PROPIEDAD_id_propiedad` ASC) VISIBLE,
  INDEX `fk_SOLICITUD_DOCUMUENTO1_idx` (`DOCUMUENTO_id_documento` ASC) VISIBLE,
  CONSTRAINT `fk_SOLICITUD_USUARIO`
    FOREIGN KEY (`USUARIO_id_usuario`)
    REFERENCES `UNIONLINE`.`USUARIO` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SOLICITUD_PROPIEDAD1`
    FOREIGN KEY (`PROPIEDAD_id_propiedad`)
    REFERENCES `UNIONLINE`.`PROPIEDAD` (`id_propiedad`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SOLICITUD_DOCUMUENTO1`
    FOREIGN KEY (`DOCUMUENTO_id_documento`)
    REFERENCES `UNIONLINE`.`DOCUMUENTO` (`id_documento`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`CAR_COMPRA`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`CAR_COMPRA` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`CAR_COMPRA` (
  `id_carrito` INT NOT NULL,
  `SOLICITUD_id_soli` INT NOT NULL,
  PRIMARY KEY (`id_carrito`, `SOLICITUD_id_soli`),
  INDEX `fk_CAR_COMPRA_SOLICITUD1_idx` (`SOLICITUD_id_soli` ASC) VISIBLE,
  CONSTRAINT `fk_CAR_COMPRA_SOLICITUD1`
    FOREIGN KEY (`SOLICITUD_id_soli`)
    REFERENCES `UNIONLINE`.`SOLICITUD` (`id_soli`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`T_PAGO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`T_PAGO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`T_PAGO` (
  `id_tipoP` INT NOT NULL,
  `nombre_tipoP` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_tipoP`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`B_PAGO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`B_PAGO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`B_PAGO` (
  `id_boleta` INT NOT NULL,
  `fecha_emision` DATETIME NOT NULL,
  `monto_pago` INT NOT NULL,
  `CAR_COMPRA_id_carrito` INT NOT NULL,
  `T_PAGO_id_tipoP` INT NOT NULL,
  PRIMARY KEY (`id_boleta`, `CAR_COMPRA_id_carrito`, `T_PAGO_id_tipoP`),
  INDEX `fk_B_PAGO_CAR_COMPRA1_idx` (`CAR_COMPRA_id_carrito` ASC) VISIBLE,
  INDEX `fk_B_PAGO_T_PAGO1_idx` (`T_PAGO_id_tipoP` ASC) VISIBLE,
  CONSTRAINT `fk_B_PAGO_CAR_COMPRA1`
    FOREIGN KEY (`CAR_COMPRA_id_carrito`)
    REFERENCES `UNIONLINE`.`CAR_COMPRA` (`id_carrito`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_B_PAGO_T_PAGO1`
    FOREIGN KEY (`T_PAGO_id_tipoP`)
    REFERENCES `UNIONLINE`.`T_PAGO` (`id_tipoP`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`ERROR`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`ERROR` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`ERROR` (
  `id_error` INT NOT NULL,
  `codigo_error` VARCHAR(45) NOT NULL,
  `mensaje_error` VARCHAR(100) NOT NULL,
  `fecha_error` DATETIME NOT NULL,
  PRIMARY KEY (`id_error`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`FORMULARIO`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`FORMULARIO` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`FORMULARIO` (
  `id_formulario` INT NOT NULL,
  `nombre_form` VARCHAR(45) NOT NULL,
  `telefono` VARCHAR(45) NOT NULL,
  `correo_form` VARCHAR(45) NOT NULL,
  `asunto_form` VARCHAR(45) NOT NULL,
  `detalle_form` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_formulario`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`T_TRAMITE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`T_TRAMITE` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`T_TRAMITE` (
  `id_tipoT` INT NOT NULL,
  `nombre_tipoT` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_tipoT`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `UNIONLINE`.`TRAMITE1`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `UNIONLINE`.`TRAMITE1` ;

CREATE TABLE IF NOT EXISTS `UNIONLINE`.`TRAMITE1` (
  `id_tramite` INT NOT NULL,
  `nombre_tramite` VARCHAR(45) NOT NULL,
  `valor_tramite` VARCHAR(45) NOT NULL,
  `SOLICITUD_id_soli` INT NOT NULL,
  `T_TRAMITE_id_tipoT` INT NOT NULL,
  PRIMARY KEY (`id_tramite`, `SOLICITUD_id_soli`, `T_TRAMITE_id_tipoT`),
  INDEX `fk_TRAMITE1_SOLICITUD1_idx` (`SOLICITUD_id_soli` ASC) VISIBLE,
  INDEX `fk_TRAMITE1_T_TRAMITE1_idx` (`T_TRAMITE_id_tipoT` ASC) VISIBLE,
  CONSTRAINT `fk_TRAMITE1_SOLICITUD1`
    FOREIGN KEY (`SOLICITUD_id_soli`)
    REFERENCES `UNIONLINE`.`SOLICITUD` (`id_soli`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TRAMITE1_T_TRAMITE1`
    FOREIGN KEY (`T_TRAMITE_id_tipoT`)
    REFERENCES `UNIONLINE`.`T_TRAMITE` (`id_tipoT`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
