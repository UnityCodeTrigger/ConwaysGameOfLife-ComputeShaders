# Conway's Game of Life using Unity Compute Shaders

Este proyecto implementa el **Juego de la Vida de Conway** utilizando **compute shaders** en Unity, con la simulación ejecutándose en la **GPU** para maximizar el rendimiento y la velocidad. Al aprovechar la capacidad de procesamiento paralelo de la GPU, este enfoque permite manejar simulaciones complejas y de alta resolución de manera más eficiente que si se ejecutara en la CPU.

![Uploading image.png…]()

## Descripción

El proyecto simula el **Juego de la Vida de Conway**, donde una cuadrícula de celdas evoluciona con base en reglas predefinidas. Las computaciones de las reglas y los efectos visuales, como el desenfoque, se ejecutan directamente en la GPU, haciendo uso de la potencia gráfica para realizar cálculos masivos en paralelo.

## Características

- **Generador de Ruido Perlin**: Inicializa el tablero con un patrón aleatorio generado por la GPU usando un compute shader.
- **Simulación en la GPU**: La evolución de las celdas (vivas o muertas) se gestiona completamente desde la GPU, calculando las interacciones entre las celdas vecinas de forma masiva y en paralelo.
- **Desenfoque de la Simulación**: Un shader compute adicional aplica un efecto de desenfoque, ejecutado en la GPU, para suavizar el resultado visual.
- **Control Interactivo**: Usa la interfaz de Unity para iniciar, pausar y avanzar manualmente la simulación, así como generar nuevas configuraciones con ruido Perlin.

## Shaders Principales

- **Perlin.compute**: Genera el ruido Perlin inicial directamente en la GPU.
- **Conway.compute**: Calcula la evolución de las celdas en paralelo en la GPU.
- **Blur.compute**: Aplica un desenfoque configurable para mejorar la estética visual de la cuadrícula.
