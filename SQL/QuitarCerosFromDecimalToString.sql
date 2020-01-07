DECLARE @prueba DECIMAL(10,2) = 5500

SELECT
Convert(VARCHAR(50), -- 7.- Deja todo a string
REPLACE
(
    RTRIM
    (
        REPLACE
        (
            REPLACE
            (
                RTRIM
                (
					REPLACE(@prueba,'0',' ') --1.- Quita todos los ceros y pone espacios vacios
                )-- 2.- Quita todos los espacios vacios de la derecha
                , ' ','0'
            ) -- 3.- Reemplaza los caracteres vacios por ceros
            ,'.',' '
        ) -- 4.- Reemplaza los puntos por espacios vacios
    ) -- 5.- Quita todos los espacios vacios de la derecha
    ,' ','.'
) -- 6.- Reemplaza los caracteres vacios por puntos
)