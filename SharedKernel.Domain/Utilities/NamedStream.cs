namespace SharedKernel.Domain.Utilities;

/// <summary>
/// Representa un Stream que mantiene información de la ruta completa del archivo asociado.
/// Hereda de Stream y delega todas las operaciones al stream interno.
/// </summary>
/// <remarks>
/// Esta clase es útil cuando necesitas pasar un Stream a métodos que requieren
/// acceso a la ruta completa del archivo original, manteniendo la compatibilidad con
/// la interfaz Stream estándar.
/// </remarks>
/// <example>
/// <code>
/// // Crear desde un stream existente
/// var namedStream = new NamedStream(existingStream, @"C:\temp\archivo.jpg");
/// await fileService.Upload(namedStream, path);
/// 
/// // Crear desde un archivo
/// var namedStream = NamedStream.FromFile(@"C:\temp\imagen.png");
/// Console.WriteLine($"Ruta completa: {namedStream.FullName}");
/// </code>
/// </example>
public class NamedStream : Stream
{
    private readonly Stream _innerStream;
    
    /// <summary>
    /// Obtiene la ruta completa del archivo asociado a este stream.
    /// </summary>
    /// <value>La ruta absoluta del archivo, incluyendo directorio y extensión.</value>
    public string FullName { get; }

    /// <summary>
    /// Obtiene el nombre del archivo asociado a este stream
    /// </summary>
    /// <value>El nombre del archivo</value>
    public string Name => Path.GetFileName(FullName);

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="NamedStream"/>.
    /// </summary>
    /// <param name="innerStream">El stream interno al cual se delegarán todas las operaciones.</param>
    /// <param name="fullName">La ruta completa del archivo asociado al stream.</param>
    /// <exception cref="ArgumentNullException">
    /// Se lanza cuando <paramref name="innerStream"/> o <paramref name="fullName"/> son null.
    /// </exception>
    public NamedStream(Stream innerStream, string fullName)
    {
        _innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
    }

    
    /// <summary>
    /// Crea una nueva instancia de <see cref="NamedStream"/> desde un archivo en el sistema de archivos.
    /// </summary>
    /// <param name="filePath">La ruta completa al archivo.</param>
    /// <returns>Una nueva instancia de <see cref="NamedStream"/> que lee desde el archivo especificado.</returns>
    /// <exception cref="ArgumentNullException">Se lanza cuando <paramref name="filePath"/> es null o vacío.</exception>
    /// <exception cref="FileNotFoundException">Se lanza cuando el archivo especificado no existe.</exception>
    /// <exception cref="UnauthorizedAccessException">Se lanza cuando no se tienen permisos para acceder al archivo.</exception>
    /// <example>
    /// <code>
    /// var namedStream = NamedStream.FromFile(@"C:\temp\imagen.png");
    /// Console.WriteLine($"Archivo: {namedStream.FullName}");
    /// </code>
    /// </example>
    public static NamedStream FromFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentNullException(nameof(filePath));
            
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return new NamedStream(fileStream, filePath);
    }

    /// <summary>
    /// Obtiene un valor que indica si el stream actual admite operaciones de lectura.
    /// </summary>
    /// <value>true si el stream admite operaciones de lectura; en caso contrario, false.</value>
    public override bool CanRead => _innerStream.CanRead;
    
    /// <summary>
    /// Obtiene un valor que indica si el stream actual admite operaciones de búsqueda.
    /// </summary>
    /// <value>true si el stream admite operaciones de búsqueda; en caso contrario, false.</value>
    public override bool CanSeek => _innerStream.CanSeek;
    
    /// <summary>
    /// Obtiene un valor que indica si el stream actual admite operaciones de escritura.
    /// </summary>
    /// <value>true si el stream admite operaciones de escritura; en caso contrario, false.</value>
    public override bool CanWrite => _innerStream.CanWrite;
    
    /// <summary>
    /// Obtiene la longitud en bytes del stream.
    /// </summary>
    /// <value>La longitud en bytes del stream.</value>
    public override long Length => _innerStream.Length;
    
    /// <summary>
    /// Obtiene o establece la posición dentro del stream actual.
    /// </summary>
    /// <value>La posición actual dentro del stream.</value>
    public override long Position 
    { 
        get => _innerStream.Position; 
        set => _innerStream.Position = value; 
    }

    /// <summary>
    /// Borra todos los búferes de este stream y hace que los datos almacenados en búfer se escriban en el dispositivo subyacente.
    /// </summary>
    public override void Flush() => _innerStream.Flush();
    
    /// <summary>
    /// Lee una secuencia de bytes del stream actual y avanza la posición dentro del stream en el número de bytes leídos.
    /// </summary>
    /// <param name="buffer">Una matriz de bytes. Cuando este método finaliza, el búfer contiene la matriz de bytes especificada con los valores entre offset y (offset + count - 1) reemplazados por los bytes leídos del origen actual.</param>
    /// <param name="offset">El desplazamiento de bytes en buffer desde el cual se comenzarán a almacenar los datos leídos del stream actual.</param>
    /// <param name="count">El número máximo de bytes que se van a leer del stream actual.</param>
    /// <returns>El número total de bytes leídos en el búfer. Este puede ser menor que el número de bytes solicitado si no hay tantos bytes disponibles, o cero (0) si se alcanzó el final del stream.</returns>
    public override int Read(byte[] buffer, int offset, int count) => 
        _innerStream.Read(buffer, offset, count);
    
    /// <summary>
    /// Establece la posición dentro del stream actual.
    /// </summary>
    /// <param name="offset">Un desplazamiento de bytes relativo al parámetro origin.</param>
    /// <param name="origin">Un valor de tipo <see cref="SeekOrigin"/> que indica el punto de referencia utilizado para obtener la nueva posición.</param>
    /// <returns>La nueva posición dentro del stream actual.</returns>
    public override long Seek(long offset, SeekOrigin origin) => 
        _innerStream.Seek(offset, origin);
    
    /// <summary>
    /// Establece la longitud del stream actual.
    /// </summary>
    /// <param name="value">La longitud deseada del stream actual en bytes.</param>
    public override void SetLength(long value) => _innerStream.SetLength(value);
    
    /// <summary>
    /// Escribe una secuencia de bytes en el stream actual y avanza la posición actual dentro del stream en el número de bytes escritos.
    /// </summary>
    /// <param name="buffer">Una matriz de bytes. Este método copia count bytes de buffer al stream actual.</param>
    /// <param name="offset">El desplazamiento de bytes en buffer desde el cual se comenzarán a copiar bytes al stream actual.</param>
    /// <param name="count">El número de bytes que se van a escribir en el stream actual.</param>
    public override void Write(byte[] buffer, int offset, int count) => 
        _innerStream.Write(buffer, offset, count);

    /// <summary>
    /// Borra de forma asíncrona todos los búferes de este stream y hace que los datos almacenados en búfer se escriban en el dispositivo subyacente.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Una tarea que representa la operación de vaciado asíncrona.</returns>
    public override Task FlushAsync(CancellationToken cancellationToken = default) => 
        _innerStream.FlushAsync(cancellationToken);

    /// <summary>
    /// Lee de forma asíncrona una secuencia de bytes del stream actual y avanza la posición dentro del stream en el número de bytes leídos.
    /// </summary>
    /// <param name="buffer">Una matriz de bytes. Cuando este método finaliza, el búfer contiene la matriz de bytes especificada con los valores entre offset y (offset + count - 1) reemplazados por los bytes leídos del origen actual.</param>
    /// <param name="offset">El desplazamiento de bytes en buffer desde el cual se comenzarán a almacenar los datos leídos del stream actual.</param>
    /// <param name="count">El número máximo de bytes que se van a leer del stream actual.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Una tarea que representa la operación de lectura asíncrona. El valor del parámetro TResult contiene el número total de bytes leídos en el búfer.</returns>
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default) => 
        _innerStream.ReadAsync(buffer, offset, count, cancellationToken);

    /// <summary>
    /// Escribe de forma asíncrona una secuencia de bytes en el stream actual y avanza la posición actual dentro del stream en el número de bytes escritos.
    /// </summary>
    /// <param name="buffer">Una matriz de bytes. Este método copia count bytes de buffer al stream actual.</param>
    /// <param name="offset">El desplazamiento de bytes en buffer desde el cual se comenzarán a copiar bytes al stream actual.</param>
    /// <param name="count">El número de bytes que se van a escribir en el stream actual.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Una tarea que representa la operación de escritura asíncrona.</returns>
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default) => 
        _innerStream.WriteAsync(buffer, offset, count, cancellationToken);

    /// <summary>
    /// Libera los recursos no administrados utilizados por la clase <see cref="NamedStream"/> y, opcionalmente, libera los recursos administrados.
    /// </summary>
    /// <param name="disposing">true para liberar tanto los recursos administrados como los no administrados; false para liberar únicamente los recursos no administrados.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _innerStream?.Dispose();
        }
        base.Dispose(disposing);
    }

}
