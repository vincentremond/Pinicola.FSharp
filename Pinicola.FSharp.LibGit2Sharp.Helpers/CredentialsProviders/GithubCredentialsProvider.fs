namespace Pinicola.FSharp.LibGit2Sharp.Helpers

open LibGit2Sharp.Handlers

type MyCredentialsHandler = delegate of 

module GithubCredentialsProvider =
    let hander: CredentialsHandler =
        (fun (url, usernameFromUrl, types) -> failwith "not implemented"

        )
