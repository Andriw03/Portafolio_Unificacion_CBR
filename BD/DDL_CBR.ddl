-- Generado por Oracle SQL Developer Data Modeler 19.1.0.081.0911
--   en:        2022-08-25 21:42:25 CLST
--   sitio:      Oracle Database 11g
--   tipo:      Oracle Database 11g



DROP TABLE b_pago CASCADE CONSTRAINTS;

DROP TABLE car_compra CASCADE CONSTRAINTS;

DROP TABLE cbr CASCADE CONSTRAINTS;

DROP TABLE clas_prop CASCADE CONSTRAINTS;

DROP TABLE comuna CASCADE CONSTRAINTS;

DROP TABLE direccion CASCADE CONSTRAINTS;

DROP TABLE documento CASCADE CONSTRAINTS;

DROP TABLE duenno_prop CASCADE CONSTRAINTS;

DROP TABLE hor_atencion CASCADE CONSTRAINTS;

DROP TABLE propiedad CASCADE CONSTRAINTS;

DROP TABLE region CASCADE CONSTRAINTS;

DROP TABLE solicitud CASCADE CONSTRAINTS;

DROP TABLE t_pago CASCADE CONSTRAINTS;

DROP TABLE t_tramite CASCADE CONSTRAINTS;

DROP TABLE t_usuario CASCADE CONSTRAINTS;

DROP TABLE tipo_documento CASCADE CONSTRAINTS;

DROP TABLE tipo_propiedad CASCADE CONSTRAINTS;

DROP TABLE tramite CASCADE CONSTRAINTS;

DROP TABLE usuario CASCADE CONSTRAINTS;

CREATE TABLE b_pago (
    id_boleta               INTEGER NOT NULL,
    fecha_emision           DATE NOT NULL,
    monto_pago              INTEGER NOT NULL,
    t_pago_id_tipop         INTEGER NOT NULL,
    car_compra_id_carrito   INTEGER NOT NULL
);

ALTER TABLE b_pago ADD CONSTRAINT b_pago_pk PRIMARY KEY ( id_boleta );

CREATE TABLE car_compra (
    id_carrito           INTEGER NOT NULL,
    usuario_id_usuario   INTEGER NOT NULL,
    solicitud_id_soli    INTEGER NOT NULL
);

ALTER TABLE car_compra ADD CONSTRAINT car_compra_pk PRIMARY KEY ( id_carrito );

CREATE TABLE cbr (
    id_cbr                    INTEGER NOT NULL,
    nombre_cbr                VARCHAR2(100 CHAR) NOT NULL,
    correo_cbr                VARCHAR2(30 CHAR) NOT NULL,
    telefono_cbr              INTEGER NOT NULL,
    direccion_id_direccion    INTEGER NOT NULL,
    hor_atencion_id_horario   INTEGER NOT NULL
);

ALTER TABLE cbr ADD CONSTRAINT cbr_pk PRIMARY KEY ( id_cbr );

CREATE TABLE clas_prop (
    id_clas                  INTEGER NOT NULL,
    foja                     INTEGER NOT NULL,
    numero                   INTEGER NOT NULL,
    anno                     DATE NOT NULL,
    propiedad_id_propiedad   INTEGER NOT NULL
);

ALTER TABLE clas_prop ADD CONSTRAINT clas_prop_pk PRIMARY KEY ( id_clas );

CREATE TABLE comuna (
    id_comuna          INTEGER NOT NULL,
    nombre_comuna      VARCHAR2(30 CHAR) NOT NULL,
    region_id_region   INTEGER NOT NULL
);

ALTER TABLE comuna ADD CONSTRAINT comuna_pk PRIMARY KEY ( id_comuna );

CREATE TABLE direccion (
    id_direccion       INTEGER NOT NULL,
    nombre_calle       VARCHAR2(50 CHAR) NOT NULL,
    numero_casa        INTEGER NOT NULL,
    comuna_id_comuna   INTEGER NOT NULL
);

ALTER TABLE direccion ADD CONSTRAINT direccion_pk PRIMARY KEY ( id_direccion );

CREATE TABLE documento (
    id_doc                      INTEGER NOT NULL,
    nombre_doc                  VARCHAR2(30 CHAR) NOT NULL,
    tipo_documento_id_tipodoc   INTEGER NOT NULL
);

ALTER TABLE documento ADD CONSTRAINT documento_pk PRIMARY KEY ( id_doc );

CREATE TABLE duenno_prop (
    id_duenno            INTEGER NOT NULL,
    rut_duenno           VARCHAR2(15 CHAR) NOT NULL,
    primer_nombre        VARCHAR2(15 CHAR) NOT NULL,
    segundo_nombre       VARCHAR2(15 CHAR) NOT NULL,
    primer_apellido      VARCHAR2(15 CHAR) NOT NULL,
    segundo_apellido     VARCHAR2(15 CHAR) NOT NULL,
    correo_electronico   VARCHAR2(30 CHAR) NOT NULL,
    telefono             VARCHAR2(12 CHAR) NOT NULL
);

ALTER TABLE duenno_prop ADD CONSTRAINT duenno_prop_pk PRIMARY KEY ( id_duenno );

CREATE TABLE hor_atencion (
    id_horario         INTEGER NOT NULL,
    dias_atencion      VARCHAR2(10 CHAR) NOT NULL,
    horario_apertura   DATE NOT NULL,
    horario_cierre     DATE NOT NULL
);

ALTER TABLE hor_atencion ADD CONSTRAINT hor_atencion_pk PRIMARY KEY ( id_horario );

CREATE TABLE propiedad (
    id_propiedad              INTEGER NOT NULL,
    nombre_propiedad          VARCHAR2(50 CHAR) NOT NULL,
    tipo_propiedad_id_tipop   INTEGER NOT NULL,
    direccion_id_direccion    INTEGER NOT NULL,
    cbr_id_cbr                INTEGER NOT NULL,
    duenno_prop_id_duenno     INTEGER NOT NULL
);

ALTER TABLE propiedad ADD CONSTRAINT propiedad_pk PRIMARY KEY ( id_propiedad );

CREATE TABLE region (
    id_region       INTEGER NOT NULL,
    nombre_region   VARCHAR2(30 CHAR) NOT NULL
);

ALTER TABLE region ADD CONSTRAINT region_pk PRIMARY KEY ( id_region );

CREATE TABLE solicitud (
    id_soli                  INTEGER NOT NULL,
    fecha_solicitud          DATE NOT NULL,
    propiedad_id_propiedad   INTEGER NOT NULL,
    tramite_id_tramite       INTEGER NOT NULL,
    documento_id_doc         INTEGER
);

ALTER TABLE solicitud ADD CONSTRAINT solicitud_pk PRIMARY KEY ( id_soli );

CREATE TABLE t_pago (
    id_tipop       INTEGER NOT NULL,
    nombre_tipop   VARCHAR2(15 CHAR) NOT NULL
);

ALTER TABLE t_pago ADD CONSTRAINT t_pago_pk PRIMARY KEY ( id_tipop );

CREATE TABLE t_tramite (
    id_tipot       INTEGER NOT NULL,
    nombre_tipot   VARCHAR2(30 CHAR) NOT NULL
);

ALTER TABLE t_tramite ADD CONSTRAINT t_tramite_pk PRIMARY KEY ( id_tipot );

CREATE TABLE t_usuario (
    id_tipou       INTEGER NOT NULL,
    nombre_tipou   VARCHAR2(15 CHAR) NOT NULL
);

ALTER TABLE t_usuario ADD CONSTRAINT t_usuario_pk PRIMARY KEY ( id_tipou );

CREATE TABLE tipo_documento (
    id_tipodoc   INTEGER NOT NULL,
    tipo_doc     VARCHAR2(40 CHAR) NOT NULL
);

ALTER TABLE tipo_documento ADD CONSTRAINT tipo_documento_pk PRIMARY KEY ( id_tipodoc );

CREATE TABLE tipo_propiedad (
    id_tipop       INTEGER NOT NULL,
    nombre_tipop   VARCHAR2(30 CHAR) NOT NULL
);

ALTER TABLE tipo_propiedad ADD CONSTRAINT tipo_propiedad_pk PRIMARY KEY ( id_tipop );

CREATE TABLE tramite (
    id_tramite           INTEGER NOT NULL,
    nombre_tramite       VARCHAR2(30 CHAR) NOT NULL,
    valor_tramite        INTEGER NOT NULL,
    t_tramite_id_tipot   INTEGER NOT NULL,
    id_boleta            INTEGER NOT NULL
);

ALTER TABLE tramite ADD CONSTRAINT tramite_pk PRIMARY KEY ( id_tramite );

CREATE TABLE usuario (
    id_usuario           INTEGER NOT NULL,
    rut_usuario          VARCHAR2(15 CHAR) NOT NULL,
    contrasenna          VARCHAR2(10 CHAR) NOT NULL,
    primer_nombre        VARCHAR2(15 CHAR) NOT NULL,
    segundo_nombre       VARCHAR2(15 CHAR) NOT NULL,
    primer_apellido      VARCHAR2(15 CHAR) NOT NULL,
    segundo_apellido     VARCHAR2(15 CHAR) NOT NULL,
    correo_electronico   VARCHAR2(30 CHAR) NOT NULL,
    telefono             VARCHAR2(12 CHAR) NOT NULL,
    t_usuario_id_tipou   INTEGER NOT NULL,
    cbr_id_cbr           INTEGER
);

ALTER TABLE usuario ADD CONSTRAINT usuario_pk PRIMARY KEY ( id_usuario );

ALTER TABLE b_pago
    ADD CONSTRAINT b_pago_car_compra_fk FOREIGN KEY ( car_compra_id_carrito )
        REFERENCES car_compra ( id_carrito );

ALTER TABLE b_pago
    ADD CONSTRAINT b_pago_t_pago_fk FOREIGN KEY ( t_pago_id_tipop )
        REFERENCES t_pago ( id_tipop );

ALTER TABLE car_compra
    ADD CONSTRAINT car_compra_solicitud_fk FOREIGN KEY ( solicitud_id_soli )
        REFERENCES solicitud ( id_soli );

ALTER TABLE car_compra
    ADD CONSTRAINT car_compra_usuario_fk FOREIGN KEY ( usuario_id_usuario )
        REFERENCES usuario ( id_usuario );

ALTER TABLE cbr
    ADD CONSTRAINT cbr_direccion_fk FOREIGN KEY ( direccion_id_direccion )
        REFERENCES direccion ( id_direccion );

ALTER TABLE cbr
    ADD CONSTRAINT cbr_hor_atencion_fk FOREIGN KEY ( hor_atencion_id_horario )
        REFERENCES hor_atencion ( id_horario );

ALTER TABLE clas_prop
    ADD CONSTRAINT clas_prop_propiedad_fk FOREIGN KEY ( propiedad_id_propiedad )
        REFERENCES propiedad ( id_propiedad );

ALTER TABLE comuna
    ADD CONSTRAINT comuna_region_fk FOREIGN KEY ( region_id_region )
        REFERENCES region ( id_region );

ALTER TABLE direccion
    ADD CONSTRAINT direccion_comuna_fk FOREIGN KEY ( comuna_id_comuna )
        REFERENCES comuna ( id_comuna );

ALTER TABLE documento
    ADD CONSTRAINT documento_tipo_documento_fk FOREIGN KEY ( tipo_documento_id_tipodoc )
        REFERENCES tipo_documento ( id_tipodoc );

ALTER TABLE propiedad
    ADD CONSTRAINT propiedad_cbr_fk FOREIGN KEY ( cbr_id_cbr )
        REFERENCES cbr ( id_cbr );

ALTER TABLE propiedad
    ADD CONSTRAINT propiedad_direccion_fk FOREIGN KEY ( direccion_id_direccion )
        REFERENCES direccion ( id_direccion );

ALTER TABLE propiedad
    ADD CONSTRAINT propiedad_duenno_prop_fk FOREIGN KEY ( duenno_prop_id_duenno )
        REFERENCES duenno_prop ( id_duenno );

ALTER TABLE propiedad
    ADD CONSTRAINT propiedad_tipo_propiedad_fk FOREIGN KEY ( tipo_propiedad_id_tipop )
        REFERENCES tipo_propiedad ( id_tipop );

ALTER TABLE solicitud
    ADD CONSTRAINT solicitud_documento_fk FOREIGN KEY ( documento_id_doc )
        REFERENCES documento ( id_doc );

ALTER TABLE solicitud
    ADD CONSTRAINT solicitud_propiedad_fk FOREIGN KEY ( propiedad_id_propiedad )
        REFERENCES propiedad ( id_propiedad );

ALTER TABLE solicitud
    ADD CONSTRAINT solicitud_tramite_fk FOREIGN KEY ( tramite_id_tramite )
        REFERENCES tramite ( id_tramite );

ALTER TABLE tramite
    ADD CONSTRAINT tramite_t_tramite_fk FOREIGN KEY ( t_tramite_id_tipot )
        REFERENCES t_tramite ( id_tipot );

ALTER TABLE usuario
    ADD CONSTRAINT usuario_cbr_fk FOREIGN KEY ( cbr_id_cbr )
        REFERENCES cbr ( id_cbr );

ALTER TABLE usuario
    ADD CONSTRAINT usuario_t_usuario_fk FOREIGN KEY ( t_usuario_id_tipou )
        REFERENCES t_usuario ( id_tipou );



-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            19
-- CREATE INDEX                             0
-- ALTER TABLE                             39
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                          0
-- CREATE MATERIALIZED VIEW                 0
-- CREATE MATERIALIZED VIEW LOG             0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
