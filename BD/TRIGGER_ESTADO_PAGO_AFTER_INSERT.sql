DELIMITER ;
USE `UNIONLINE`;

DELIMITER $$

USE `UNIONLINE`$$
DROP TRIGGER IF EXISTS `UNIONLINE`.`ESTADO_PAGO_AFTER_INSERT` $$
USE `UNIONLINE`$$
CREATE DEFINER=`root`@`%` TRIGGER `ESTADO_PAGO_AFTER_INSERT` AFTER INSERT ON `ESTADO_PAGO` FOR EACH ROW BEGIN
declare var_monto INTEGER; 
declare tipoP varchar(50);
SELECT nombre_tipoP into tipoP FROM UNIONLINE.ESTADO_PAGO inner join UNIONLINE.TIPO_PAGO on UNIONLINE.ESTADO_PAGO.TIPO_PAGO_id_tipoP = UNIONLINE.TIPO_PAGO.id_tipoP where id_estado = NEW.id_estado;
set var_monto = FN_CALCULAR_MONTO(NEW.id_estado);
INSERT INTO  `UNIONLINE`. `B_PAGO` (`fecha_emision`,`monto_pago`,`tipo_pago`,`orden_pago`) VALUES (now(),var_monto,tipoP,new.n_boleta);
UPDATE `UNIONLINE`.`CAR_COMPRA` SET `estado` = 1 WHERE `id_carrito` = NEW.CAR_COMPRA_id_carrito;
END$$

