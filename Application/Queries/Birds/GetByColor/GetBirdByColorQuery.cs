using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Birds.GetByColor
{
    public class GetBirdByColorQuery : IRequest<List<Bird>>
    {
        public string Color { get; }

        public GetBirdByColorQuery(string color)
        {
            Color = color;
        }

    }
}
