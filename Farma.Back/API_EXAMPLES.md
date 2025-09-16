# Farma API - Ejemplos de Uso

## Autenticación

La API soporta dos métodos de autenticación:

### Basic Auth
```
Username: admin
Password: admin123
```

### Bearer Token
```
Token: farma-api-key-2025
```

## Endpoints Disponibles

### 1. Health Check
```http
GET /
```

### 2. Test de Autenticación
```http
POST /auth/basic-test
Authorization: Basic YWRtaW46YWRtaW4xMjM=
```

### 3. Crear Receta (Multipart)
```http
POST /recetas
Authorization: Bearer farma-api-key-2024
Content-Type: multipart/form-data

Form Data:
- archivo: [archivo de receta]
- calle: "Av. Corrientes"
- altura: "1234"
- piso: "5" (opcional)
- depto: "A" (opcional)
- localidad: "Buenos Aires"
- provincia: "Buenos Aires"
- codigoPostal: "1043"
- lat: -34.6037 (opcional)
- lon: -58.3816 (opcional)
- usuarioId: 1
```

### 4. Obtener Pedido por ID
```http
GET /pedidos/1
Authorization: Bearer farma-api-key-2024
```

### 5. Obtener Pedidos por Estado
```http
GET /pedidos?estado=EN_COTIZACION
Authorization: Bearer farma-api-key-2024
```

### 6. Cancelar Pedido
```http
POST /pedidos/1/cancelar
Authorization: Bearer farma-api-key-2024
```

### 7. Farmacias Cercanas (con coordenadas)
```http
GET /farmacias/cercanas?lat=-34.6037&lng=-58.3816&radio=5&limite=10
Authorization: Bearer farma-api-key-2024
```

### 8. Farmacias Cercanas (fallback por ubicación)
```http
GET /farmacias/cercanas?localidad=Buenos Aires&provincia=Buenos Aires&codigoPostal=1043
Authorization: Bearer farma-api-key-2024
```

### 9. Obtener Todas las Farmacias
```http
GET /farmacias
Authorization: Bearer farma-api-key-2024
```

## Estados de Pedido
- `CREADO`
- `EN_COTIZACION`
- `COTIZADO`
- `CONFIRMADO`
- `EN_PREPARACION`
- `LISTO_PARA_ENTREGA`
- `EN_ENTREGA`
- `ENTREGADO`
- `CANCELADO`

## Tipos de Archivo Permitidos
- `.jpg`, `.jpeg`, `.png` (imágenes)
- `.pdf` (documentos)
- `.doc`, `.docx` (Word)

## Límites
- Tamaño máximo de archivo: 10MB
- Radio máximo de búsqueda: Sin límite (recomendado: 50km)
- Límite de resultados: 20 por defecto

## Usuarios de Prueba
1. Juan Pérez (ID: 1) - juan.perez@email.com
2. María García (ID: 2) - maria.garcia@email.com
3. Carlos López (ID: 3) - carlos.lopez@email.com

## Farmacias de Prueba
1. Farmacia Central - Av. Corrientes 1234, Buenos Aires
2. Farmacia del Barrio - Av. Santa Fe 5678, Buenos Aires

## Estructura de Carpetas de Archivos
```
data/
└── recetas/
    └── 2024/
        └── 01/
            ├── guid1.jpg
            ├── guid2.pdf
            └── ...
```
