using TesisApi.Domain.Entities;

namespace TesisApi.Data;

/// <summary>
/// 10 semillas: 4 tutores + 10 tesis (3 + 3 + 3 + 1), respetando el máximo de 3 tesis por tutor.
/// </summary>
public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Tutores.Any()) return;

        var tutores = new List<Tutor>
        {
            new() { Nombre = "Dra. Carla Mendoza", Especialidad = "Inteligencia Artificial", Correo = "cmendoza@uni.edu" },
            new() { Nombre = "Ing. Roberto Quispe", Especialidad = "Redes y Seguridad", Correo = "rquispe@uni.edu" },
            new() { Nombre = "Dr. Luis Fernández", Especialidad = "Bases de Datos", Correo = "lfernandez@uni.edu" },
            new() { Nombre = "MSc. Andrea Vargas", Especialidad = "Ingeniería de Software", Correo = "avargas@uni.edu" }
        };
        db.Tutores.AddRange(tutores);
        db.SaveChanges();

        Tesis T(string titulo, string est, EstadoTesis estado, Tutor tutor) =>
            new() { Titulo = titulo, Estudiante = est, Estado = estado, IdTutor = tutor.Id };

        db.Tesis.AddRange(
            T("Sistema de reconocimiento de imágenes médicas con redes neuronales", "Juan Pérez", EstadoTesis.EnProgreso, tutores[0]),
            T("Chatbot académico basado en procesamiento de lenguaje natural", "María López", EstadoTesis.Pendiente, tutores[0]),
            T("Predicción de deserción estudiantil con aprendizaje automático", "Carlos Rojas", EstadoTesis.Aprobada, tutores[0]),

            T("Detección de intrusos en redes corporativas mediante análisis de tráfico", "Ana Torres", EstadoTesis.EnProgreso, tutores[1]),
            T("Implementación de una VPN segura para teletrabajo", "Diego Mamani", EstadoTesis.Aprobada, tutores[1]),
            T("Auditoría de seguridad en aplicaciones web universitarias", "Lucía Flores", EstadoTesis.Pendiente, tutores[1]),

            T("Optimización de consultas en un data warehouse académico", "Pedro Vaca", EstadoTesis.EnProgreso, tutores[2]),
            T("Migración de base de datos relacional a arquitectura híbrida", "Sofía Cruz", EstadoTesis.Rechazada, tutores[2]),
            T("Diseño de un modelo de datos para historiales clínicos", "Jorge Salinas", EstadoTesis.Aprobada, tutores[2]),

            T("Arquitectura de microservicios para un sistema de matriculación", "Valeria Gómez", EstadoTesis.EnProgreso, tutores[3])
        );
        db.SaveChanges();
    }
}
