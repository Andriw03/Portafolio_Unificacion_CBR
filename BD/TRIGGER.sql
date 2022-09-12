USE `UNIONLINE`$$
DROP TRIGGER IF EXISTS `UNIONLINE`.`ESTADO_PAGO_AFTER_INSERT` $$
USE `UNIONLINE`$$
CREATE DEFINER = CURRENT_USER TRIGGER `UNIONLINE`.`ESTADO_PAGO_AFTER_INSERT` AFTER INSERT ON `ESTADO_PAGO` FOR EACH ROW
BEGIN
	IF FN_AGREGAR_BOLETA (NEW.id_estado) then
		INSERT INTO  `UNIONLINE`. `B_PAGO` (`fecha_emision`,`monto_pago`,`tipo_pago`,`nombre_conservador`)
		VALUES
		(now(),monto_total,tipoP,conservador);
	ELSE
		INSERT INTO  `UNIONLINE`. `ERROR` (`codigo_error`,`mensaje_error`,`fecha_error`)
		VALUES
		('1002','Carrito de compra sin monto',now());
	END IF;
END$$