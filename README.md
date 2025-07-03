# AcademicEnrollmentApi

API profesional para la gestión de inscripciones universitarias, siguiendo DDD y arquitectura en capas.

## Arquitectura
- **API (Presentación):** Controllers, DTOs, configuración de servicios.
- **Application:** Servicios e interfaces de aplicación.
- **Domain:** Entidades y lógica de negocio.
- **Infrastructure:** Repositorios, DbContext, DI, acceso a datos.

## Requisitos
- .NET 8 SDK
- PostgreSQL
- Docker (opcional)

## Configuración Inicial
1. Clona el repositorio:
   ```bash
   git clone <repo-url>
   cd AcademicEnrollmentApi
   ```
2. Restaura dependencias:
   ```bash
   dotnet restore
   ```
3. Configura la cadena de conexión en `appsettings.Development.json`.

## Base de Datos
1. Aplica migraciones:
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef migrations add InitialCreate --project Infrastructure --startup-project AcademicEnrollmentApi
   dotnet ef database update --project Infrastructure --startup-project AcademicEnrollmentApi
   ```

## Ejecución del Proyecto
```bash
cd AcademicEnrollmentApi
 dotnet run
```
Accede a Swagger en: [http://localhost:5000/swagger](http://localhost:5000/swagger) (o el puerto configurado)

## Ejecución con Docker
```bash
docker build -t academic-enrollment-api .
docker run -p 8080:8080 academic-enrollment-api
```

## Endpoints Principales
### Autenticación
- `POST /api/auth/token` — Obtiene un JWT (admin/password123)

### Estudiantes
- `POST /api/students` — Crear estudiante
- `GET /api/students` — Listar estudiantes (paginado)
- `GET /api/students/{id}` — Obtener estudiante por Id
- `PUT /api/students/{id}` — Actualizar estudiante
- `DELETE /api/students/{id}` — Eliminar estudiante (si no tiene inscripciones)

### Inscripciones
- `POST /api/enrollments` — Nueva inscripción semestral
- `POST /api/enrollments/{semesterId}/courses` — Inscribir curso
- `GET /api/enrollments/{semesterId}` — Detalles de inscripción
- `GET /api/enrollments/student/{studentId}` — Historial de inscripciones

## Seguridad
- Todos los endpoints (excepto autenticación) requieren JWT Bearer.
- Swagger permite autenticarse con JWT para probar endpoints protegidos.

---

© 2024 AcademicEnrollmentApi 