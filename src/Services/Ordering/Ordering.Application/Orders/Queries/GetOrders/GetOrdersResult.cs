<<<<<<< HEAD
﻿namespace Ordering.Application.Orders.Queries.GetOrders;
=======
﻿using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);