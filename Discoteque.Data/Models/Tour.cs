using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public  class Tour : BaseEntity<int>
{

    /// <summary>
    /// Name of the Tour
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Name of the City of the Tour
    /// </summary>
    public string City { get; set; } = "";

    /// <summary>
    /// Date of the Tour
    /// </summary>
    public string Date { get; set;} = "";

    public bool IsSold{ get; set; }
    
    /// <summary>
    /// The <see cref="Artist"/> id this Tour belongs to
    /// </summary>
    /// <value></value>
    [ForeignKey("Id")]
    public int ArtistId { get; set; }

    /// <summary>
    /// The <see cref="Artist"/> Entity this Tour is refering to
    /// </summary> <summary>
    public virtual Artist? Artist { get; set; } 
}
