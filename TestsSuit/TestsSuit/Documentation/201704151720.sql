--use CAMPOI

WITH permisos (permissionIdBranch, permissionIdLeaf)
AS
(
-- Anchor member definition
    SELECT
		pr.permissionIdBranch, pr.permissionIdLeaf
	FROM		
		privilege pr
	WHERE 
		pr.permissionIdBranch = 'OP002'
    UNION ALL
-- Recursive member definition
SELECT 
		pr.permissionIdBranch, pr.permissionIdLeaf
	FROM
		privilege pr
	INNER JOIN 
		permisos p
	ON p.permissionIdLeaf = pr.permissionIdBranch


)

-- Al final queda: id del permiso, descripción, permiso por el que se accedió, padre // autoreferencia
SELECT v.*, pe.description FROM 
	(SELECT  p.* FROM permisos p UNION SELECT NULL,  'OP002') v	
LEFT JOIN
	permission pe
ON  pe.id = v.permissionIdLeaf


--WHERE 
--permissionIdBranch = 'OP002' or (id = 'OP002' and permissionIdBranch is  null)

