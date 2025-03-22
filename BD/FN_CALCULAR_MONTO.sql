USE `UNIONLINE`;
DROP function IF EXISTS `UNIONLINE`.`FN_CALCULAR_MONTO`;

DELIMITER $$
USE `UNIONLINE`$$
CREATE DEFINER=`root`@`%` FUNCTION `FN_CALCULAR_MONTO`(estado int) RETURNS INT
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