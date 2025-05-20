namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console
open Spectre.Console.Rendering

[<RequireQualifiedAccess>]
module Panel =

    let fromString (s: string) = Panel(s)
    let fromRenderable (r: IRenderable) = Panel(r)

    let withBorder b (panel: Panel) =
        panel.Border <- b
        panel

    let withHeader (header: PanelHeader) (panel: Panel) =
        panel.Header <- header
        panel
        
    let withExpand (expand: bool) (panel: Panel) =
        panel.Expand <- expand
        panel
        
    let withPadding (padding: Padding) (panel: Panel) =
        panel.Padding <- padding
        panel
