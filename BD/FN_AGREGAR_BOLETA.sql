USE `UNIONLINE`;
DROP function IF EXISTS `FN_AGREGAR_BOLETA`;

USE `UNIONLINE`;
DROP function IF EXISTS `UNIONLINE`.`FN_AGREGAR_BOLETA`;
;

DELIMITER $$
USE `UNIONLINE`$$
CREATE DEFINER=`root`@`%` FUNCTION `FN_AGREGAR_BOLETA`(estado int) RETURNS VARCHAR(20)
BEGIN
	declare monto_total int;
    declare conservador int;
    declare tipoP int;
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
SELECT 
    ESTADO_PAGO.TIPO_PAGO_id_tipoP,
    SOLICITUD.PROPIEDAD_id_propiedad
INTO conservador , tipoP FROM
    UNIONLINE.ESTADO_PAGO
        INNER JOIN
    UNIONLINE.CAR_COMPRA ON ESTADO_PAGO.CAR_COMPRA_id_carrito = CAR_COMPRA.id_carrito
        INNER JOIN
    UNIONLINE.SOLICITUD ON CAR_COMPRA.SOLICITUD_id_soli = SOLICITUD.id_soli
WHERE
    ESTADO_PAGO.id_estado = estado;

IF monto_total > 0 THEN
	INSERT INTO  `UNIONLINE`. `B_PAGO` (`fecha_emision`,`monto_pago`,`tipo_pago`,`nombre_conservador`)
    VALUES
    (now(),monto_total,tipoP,conservador);
    RETURN "Éxito";
    
ELSE 
	INSERT INTO  `UNIONLINE`. `ERROR` (`codigo_error`,`mensaje_error`,`fecha_error`)
    VALUES
    ('1002','Carrito de compra sin monto',now());
    RETURN "Error";
END IF;
END$$

DELIMITER ;
;

