{ pkgs, atomi, pkgs-2411 }:
let
  all = {
    atomipkgs = (
      with atomi;
      {
        inherit
          sg
          pls;
      }
    );
    nix-unstable = (
      with pkgs;
      { }
    );
    nix-2411 = (
      with pkgs-2411;
      {

        yq = yq-go;
        dotnet = dotnet-sdk_9;
        jdk = zulu17;

        inherit

          # standard
          coreutils
          findutils
          gnugrep
          gnused
          gcc
          jq
          bash

          git
          curl
          infisical

          xmlstarlet

          treefmt
          gitlint
          shellcheck
          ;
      }
    );

  };
in
with all;
nix-2411 //
nix-unstable //
atomipkgs

