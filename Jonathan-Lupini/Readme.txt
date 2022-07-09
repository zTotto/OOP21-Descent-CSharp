Nel folder Tasks.Dummy ci sono le classi d'Audio convertite in C# ma non usate in quanto fortemente legate a librerie/third party programs, il folder Supporting raccoglie
classi fittizie usate per i metodi delle classe in Dummy.

Nel folder Tasks.Working ci sono classi funzionanti dell'AI che si attengono il piu possibile alla versione Java, il folder Supporting raccoglie classi necessarie per definire il dominio su cui l'AI opera (e.g. character, map...) che differiscono in implementazione dalla versione Java per mancanza di librerie. 
Nella classe Controller c'è il metodo start che renderizza su console una demo del mob (M) che si muove randomicamente fino a quando vede l'eroe (H), 
una volta visto l'eroe il mob cerca di avvicinarsi e di ucciderlo.
