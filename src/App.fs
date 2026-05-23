// taidalog's portfolio
// https://github.com/taidalog/taidalog.github.io
// Copyright (c) 2023-2026 taidalog
// This software is licensed under the MIT License.
// https://github.com/taidalog/taidalog.github.io/blob/main/LICENSE

namespace TaidalogsPortfolio

open System
open Browser.Dom
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open SnowFlake

module App =
    window.addEventListener (
        "DOMContentLoaded",
        (fun _ ->
            // information policy window
            (document.getElementById "informationPolicyLink").onclick <-
                fun event ->
                    event.preventDefault ()
                    (document.getElementById "informationPolicyWindow")?togglePopover ()

            // regex explanation popover
            (document.getElementById "regexExplanationSpan").onclick <-
                fun _ -> (document.getElementById "regexExplanation")?togglePopover ()

            (document.getElementById "snowSection").setAttribute ("width", string document.body.clientWidth)

            (document.getElementById "snowSection").setAttribute ("height", string document.body.scrollHeight)

            // snow falling
            [ 0..17 ] |> List.iter (fun _ -> fall false)

            (document.getElementById "umbrellaOpen").onclick <-
                fun _ ->
                    (document.getElementById "umbrellaOpen").classList.toggle "display-none"
                    |> ignore

                    (document.getElementById "umbrellaFolded").classList.toggle "display-none"
                    |> ignore

                    let snowFlakeElements: Element array =
                        document.getElementsByClassName "snow-flake"
                        |> fun x -> JS.Constructors.Array?from(x)

                    snowFlakeElements
                    |> Array.iter (fun x -> x.classList.add "fading-out" |> ignore)

                    setTimeout
                        (fun _ ->
                            snowFlakeElements
                            |> Array.iter (fun x -> x.parentElement.removeChild x |> ignore))
                        3000
                    |> ignore

            (document.getElementById "umbrellaFolded").onclick <-
                fun _ ->
                    (document.getElementById "umbrellaFolded").classList.toggle "display-none"
                    |> ignore

                    (document.getElementById "umbrellaOpen").classList.toggle "display-none"
                    |> ignore

                    [ 0..17 ] |> List.iter (fun _ -> fall false)

            let today = DateTime.Today

            if (today.Month = 3 && today.Day = 1) || (today.Month = 4 && today.Day < 8) then
                document.body.classList.add "spring"
                (document.getElementById "fsharpDescriptionVariable").innerText <- "このページの桜もF#で舞わせています。"
            else
                document.body.classList.remove "spring"
                (document.getElementById "fsharpDescriptionVariable").innerText <- "このページの雪もF#で降らせています。")
    )

    window.onresize <-
        fun _ ->
            let snowSection = document.getElementById "snowSection"
            snowSection.setAttribute ("width", string document.body.clientWidth)
            snowSection.setAttribute ("height", string document.body.scrollHeight)

            let snowSvgs: Element array =
                snowSection.getElementsByClassName "snow-svg"
                |> fun x -> JS.Constructors.Array?from(x)

            snowSvgs
            |> Array.iter (fun x ->
                x.setAttribute ("width", string document.body.clientWidth)
                x.setAttribute ("height", string document.body.scrollHeight))
