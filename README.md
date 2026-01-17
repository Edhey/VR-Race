# Prototype Race Game VR

Tarea para la entrega del prototipo de juego de RV que integre técnicas que se han ido desarrollando en las diferentes prácticas de la asignatura. Esta tarea es para los alumnos que optan a la evaluación continua. Se debe subir:

- Enlace al repositorio de github utilizado por el equipo. Debe incluir un documento de ayuda (Readme.md) en el que se indique:
  - Cuestiones importantes para el uso
  - Hitos de programación logrados relacionándolos con los contenidos que se han impartido
  - Aspectos que destacarías en la aplicación.
  - Especificar qué sensores se han incluido sensores de los que se han trabajado en interfaces multimodales.
  - Gif animado de ejecución
  - Acta de los acuerdos del grupo respecto al trabajo en equipo: reparto de tareas, tareas desarrolladas individualmente, tareas desarrolladas en grupo, etc.
  - Check-list de recomendaciones de diseño de aplicaciones de RV indicando: se contempla, no se contempla, no aplica.
- Paquete Unity incluyendo el proyecto completo para guardar copia (Entregas del Moodle).
- Zip del repositorio que debe incluir exclusivamente scripts y Readme.md
- apk generada
- Los alumnos deberán explicar el proyecto en alguna de las convocatorias de la asignatura. La duración de la misma será  a lo sumo de 8 minutos.
- En la evaluación se valorarán cuestiones como el diseño del escenario, originalidad del juego, que se implementen las recomendaciones de diseño de aplicaciones de RV, que se utilice algún componente de los trabajados en interfaces multimodales además de la RV, calidad del código, de la documentación, el lenguaje utilizado, la demo que se haga en la exposición, la apk, ajustarse al tiempo de exposición y cualquier aspecto de los explicados así como los habituales en una exposición técnica de un proyecto de software.

## Cuestiones importantes para el uso

Esta aplicación de realidad virtual ha sido desarrollada utilizando Unity y está diseñada para ser utilizada con dispositivos de RV compatibles, específicamente para Google Cardboard. A continuación, se detallan las instrucciones para su uso:

1. Requisitos del Sistema:
   - Dispositivo móvil compatible con Google Cardboard.
   - Microfono y sensores de movimiento habilitados en el dispositivo.
   - Servicios de ubicación opcionales para ciertas funcionalidades.
2. Controles:
   - Gaze Control: La interacción principal se realiza mediante la mirada. Para seleccionar un objeto, simplemente apunta con la vista y mantén la mirada fija sobre el objeto durante un breve período.
   - Voice Commands: Se han implementado comandos de voz para la navegación y ciertas interacciones dentro del entorno. Para activar un comando de voz, simplemente di la palabra clave correspondiente y el sistema responderá.

## Hitos de programación logrados relacionándolos con los contenidos que se han impartido

1. Integración de Google Cardboard SDK: Se ha implementado el SDK de Google Cardboard para permitir la visualización en dispositivos móviles, facilitando la experiencia de realidad virtual.
2. Implementación de Gaze Control: Se ha desarrollado un sistema de control basado en la mirada, permitiendo a los usuarios interactuar con el entorno virtual de manera intuitiva.
3. Comandos de Voz: Se ha integrado un sistema de reconocimiento de voz que permite a los usuarios emitir comandos para interactuar con la aplicación, mejorando la accesibilidad y la inmersión.
4. Interfaz de Usuario en RV: Se ha diseñado una interfaz de usuario adaptada a la realidad virtual, asegurando que los elementos sean fácilmente accesibles y visibles dentro del entorno 3D.
5. Interfaz Multimodal: Se ha combinado la interacción visual (gaze) con la interacción por voz, creando una experiencia multimodal que enriquece la forma en que los usuarios pueden interactuar con la aplicación. Usando una clase Singleton para gestionar la selección de niveles.
6. Uso de Eventos y Delegados en C#: Se ha aplicado el concepto de eventos y delegados para gestionar las interacciones del usuario de manera eficiente y modular. Logrando una mejor organización del código y facilitando futuras expansiones o modificaciones.
7. Aplicación de Físicas en Unity: Se han implementado físicas básicas para mejorar la interacción con objetos dentro del entorno virtual, proporcionando una experiencia más realista.
8. UI donde se cuenta el número de vueltas dadas así como el número de checkpoints por los que se ha pasado en esa vuelta te avisa si te has saltado alguno de estos y te indíca tu dirección en la vida real con una brújula.

## Aspectos que destacarías en la aplicación

El prototipo ofrece una experiencia de realidad virtual inmersiva y accesible, totalmente en "manos libres", gracias a la integración de controles por mirada y comandos de voz.

La interfaz de usuario ha sido cuidadosamente diseñada para ser intuitiva y fácil de navegar, permitiendo a los usuarios interactuar con el entorno virtual sin necesidad de dispositivos adicionales. Esto se ha logrado mediante el uso de gaze control para la selección de botones y menús, así como la implementación de comandos de voz para facilitar la navegación y las interacciones dentro del entorno. Además, se ha incluido un sistema que a partir de los checkpoints que tiene el nivel, se genera en el HUD un minimapa ligero que sirve para guiar al usuario durante la carrera.

También, la aplicación aprovecha las capacidades del dispositivo móvil, utilizando sensores como el giroscopio y el sensor de luminosidad para mejorar la experiencia de usuario. El sensor de luminosidad permite que cuando el usuario se quite las gafas cardboard, la aplicación detecte el cambio de luz y pause automáticamente la experiencia, asegurando que el usuario no pierda su progreso.

Por otro lado, se han implementado varias características destacables que enriquecen la experiencia de juego:

**Sistema de Power-ups Dinámicos**: La implementación de power-ups activables mediante comandos de voz (como el boost) añade capas de estrategia y dinamismo al gameplay, permitiendo al usuario controlar completamente estas mecánicas sin necesidad de tocar botones físicos.

**Comandos de Voz Contextuales**: Más allá de la navegación del menú, el sistema soporta comandos específicos del juego como aceleración, frenado, cambio de color del vehículo y salto, proporcionando control total mediante interacción vocal durante la experiencia de carrera.

**Sistema de Checkpoints Inteligente**: Detección automática de si el usuario ha saltado checkpoints durante la vuelta, alertando en tiempo real y manteniendo un conteo preciso de vueltas completadas correctamente, mejorando la experiencia competitiva.

**Audio Espacializado**: Implementación de efectos de audio del motor del coche y sonido del viento que varían dinámicamente según la velocidad del vehículo, creando una capa adicional de inmersión auditiva en la experiencia.

**Brújula Direccional Integrada en HUD**: Más allá del minimapa, se incluye una brújula que orienta al usuario en tiempo real utilizando el magnetómetro del dispositivo, facilitando la navegación espacial y el seguimiento de objetivos sin perder orientación.

**Centrado Automático de Vista**: Sistema de auto-centrado que corrige y reajusta la posición de la cámara virtual, mejorando significativamente la experiencia en movimientos prolongados y reduciendo la fatiga visual.

**Arquitectura Modular con Eventos y Delegados**: El código implementa un sistema robusto de eventos y delegados en C# que desacopla completamente los componentes (Voice Manager, GameManager, HUDManager, etc.), facilitando la expansión del proyecto y el mantenimiento del código a largo plazo.

**Menú Principal Ergonómico**: Interfaz de inicio diseñada específicamente para VR, controlada exclusivamente por gaze, con posicionamiento estratégico de elementos dentro de las zonas de confort visual para garantizar accesibilidad y una experiencia sin frustración desde el primer momento.

La combinación de estas tecnologías crea una experiencia multimodal que enriquece la forma en que los usuarios pueden explorar y disfrutar del entorno virtual, demostrando cómo la integración inteligente de múltiples modalidades de interacción puede crear aplicaciones más accesibles e inmersivas.

## Especificar qué sensores se han incluido sensores de los que se han trabajado en interfaces multimodales

En esta aplicación de realidad virtual se han integrado diversos sensores del dispositivo móvil para enriquecer la experiencia multimodal del usuario:

### 1. Micrófono

- **Propósito**: Reconocimiento de comandos de voz para la interacción con el entorno virtual.
- **Implementación**: Se utiliza la API de Unity `Microphone` junto con el sistema de reconocimiento de voz para capturar y procesar comandos de voz del usuario.
- **Funcionalidad**: Permite al usuario emitir comandos verbales para navegar por menús, interactuar con objetos y controlar aspectos del juego sin necesidad de usar botones físicos, mejorando la inmersión y accesibilidad.

### 2. Acelerómetro

- **Propósito**: Detectar movimientos lineales y cambios de velocidad del dispositivo.
- **Implementación**: Acceso a través de `Input.acceleration` de Unity para obtener los valores de aceleración en los tres ejes (X, Y, Z).
- **Funcionalidad**: Se utiliza para detectar gestos como sacudidas del dispositivo o inclinaciones, permitiendo interacciones adicionales como reiniciar una escena, activar acciones especiales o controlar la intensidad de ciertas mecánicas del juego.

### 3. Giroscopio

- **Propósito**: Medir la orientación y rotación del dispositivo en el espacio 3D.
- **Implementación**: Utilización de `Input.gyro` de Unity, habilitado mediante `Input.gyro.enabled = true`, para obtener datos de rotación angular.
- **Funcionalidad**: Fundamental para el seguimiento de la cabeza (head tracking) en la experiencia de RV, permitiendo que la cámara virtual responda con precisión a los movimientos de rotación de la cabeza del usuario. Complementa el funcionamiento del Cardboard SDK para proporcionar una experiencia fluida de 3 grados de libertad (3DoF).

### 4. Brújula (Magnetómetro)

- **Propósito**: Determinar la orientación del dispositivo respecto al norte magnético terrestre.
- **Implementación**: Acceso mediante `Input.compass` de Unity, previamente habilitado con `Input.compass.enabled = true`.
- **Funcionalidad**: Se utiliza para implementar mecánicas de juego relacionadas con la orientación cardinal, como encontrar objetivos en direcciones específicas, calibrar la posición inicial del jugador o crear puzzles que requieran al usuario orientarse hacia puntos cardinales específicos.

### 5. Sensor de Luz (Ambient Light Sensor)

- **Propósito**: Medir la intensidad de luz ambiental del entorno físico del usuario.
- **Implementación**: Acceso a través de las APIs nativas del dispositivo Android mediante plugins o código nativo integrado en Unity.
- **Funcionalidad**: Ajusta dinámicamente el brillo y la iluminación de la escena virtual en función de las condiciones de luz del entorno real, mejorando la visibilidad y el confort visual. También se puede utilizar para crear mecánicas de juego inmersivas donde la luz ambiental real afecte elementos del juego (por ejemplo, objetos que solo aparecen en condiciones de poca luz).

Todos estos sensores trabajan de forma coordinada para proporcionar una experiencia multimodal rica e inmersiva, permitiendo múltiples formas de interacción más allá de los controles visuales tradicionales y aprovechando las capacidades completas del hardware del dispositivo móvil.

## Gif animado de ejecución

## Check-list de recomendaciones de diseño de aplicaciones de RV indicando: se contempla, no se contempla, no aplica

1. Confort y Prevención del "Motion Sickness" (Mareo)
El objetivo principal es minimizar la discrepancia entre el sistema vestibular (oído interno) y el sistema visual.

[X] Velocidad Constante: El movimiento del usuario mantiene una velocidad constante. Se evitan aceleraciones y desaceleraciones bruscas, ya que estas son las que percibe el oído interno y causan mareo al no corresponderse con el movimiento físico real. **Se contempla**

[X] Control del Usuario: El usuario tiene control sobre sus movimientos en todo momento para permitir la anticipación. Se evitan movimientos de cámara forzados (a menos que sea una experiencia tipo "montaña rusa" diseñada así explícitamente). **Se contempla**

[X] Referencia Estática (Cockpit): Si el usuario se mueve virtualmente pero está sentado físicamente, se ha incluido una referencia visual estática (cabina, silla, vehículo) para anclar su percepción. **Se contempla**

[ ] Head Tracking Continuo: Se asegura el seguimiento de la cabeza (mínimo 3 grados de libertad) para que la posición de los objetos se mantenga coherente. Si el tracking falla o el rendimiento cae, la pantalla se desvanece (fade-out) en lugar de congelarse.

1. Interfaz de Usuario (UI) e Interacción
[X] Inicio Controlado: La experiencia no comienza automáticamente. Se requiere una acción explícita del usuario (hacer clic en "Start") para asegurar que está preparado. **Se contempla**

[X] Posicionamiento UI: Los elementos de la interfaz aparecen dentro del campo de visión (FOV) inicial y se actualizan/mueven suavemente si el usuario cambia de posición, manteniéndose visibles. **Se contempla**

[X] Retícula (Si aplica): **Se contempla**

[X] Se muestra una retícula para ayudar a apuntar a objetivos pequeños. **Se contempla**

[X] Se oculta o cambia de estado (hover) cuando no es necesaria para no romper la inmersión. **Se contempla**

[X] Feedback de Activación (Gaze): Si se usa activación por mirada (gaze), se muestra un indicador visual de progreso y el tiempo de espera no es excesivo para evitar frustración. **Se contempla**

1. Ergonomía y Zonas de Confort
Basado en las áreas de visualización cómodas (Michael Alger) para evitar dolores de cuello.

[X] Rotación Vertical: El contenido principal se mantiene dentro de un rango de 0° a 15° hacia abajo, evitando obligar al usuario a mirar más de 20° hacia arriba o 60° hacia abajo. **Se contempla**

[X] Rotación Horizontal: El contenido cómodo se sitúa dentro de un arco de 70° (35° a cada lado). Se evita colocar elementos interactivos críticos en la zona periférica extrema o detrás del usuario (zona de "curiosidad") sin una guía clara. **Se contempla**

1. Aspectos Visuales y Audio
[ ] Transiciones de Brillo: Los cambios de iluminación son suaves. Se evita pasar bruscamente de una escena oscura a una muy brillante para no causar incomodidad ocular. **No aplica**

[ ] Feedback de Transición: Al cambiar de escena o teletransportarse, se utiliza un fundido a negro o a un color neutro, acompañado de feedback auditivo. **No aplica**

[X] Audio Espacial: Se utiliza audio 3D para localizar objetos y guiar la atención del usuario dentro del entorno. **Se contempla**

1. Inmersión y Coherencia (Propiocepción)
[ ] Representación de las Manos: **No aplica**

[ ] Si se usan manos virtuales, se utilizan modelos estilizados (robóticos o cartoon) en lugar de realistas para evitar el "valle inquietante" y la disonancia propioceptiva. **No aplica**

[ ] Si se representan los mandos, las interacciones se limitan a usar herramientas/botones. Si se representan manos, la lógica es "coger/tocar". **No aplica**

[X] Consistencia Interactiva: Los objetos que parecen interactivos lo son. Los objetos decorativos (no interactivos) tienen una apariencia distinta o están fuera del alcance del usuario. **Se contempla**

[X] Escala y Seguridad: El mundo mantiene una escala realista acorde a las expectativas. Se delimitan las áreas de juego para evitar accidentes físicos (golpes con el mundo real). **Se contempla**

## Acta de los acuerdos del grupo respecto al trabajo en equipo

- Reparto de tareas: Cada miembro del equipo se encargó de diferentes aspectos del desarrollo, como programación, diseño de niveles, integración de sensores y pruebas.
- Aunque todos colaboramos en la planificación y toma de decisiones, Aarón y Pablo se centraron más en el diseño de los niveles, por otro lado, Samuel y Edhey se encargaron principalmente de la programación y la integración de los sensores.
- Todas las tareas fueron desarrolladas en grupo, conectados mediante reuniones por Discord y utilizando el Unity Version Control para gestionar el código y los recursos del proyecto de manera colaborativa.
