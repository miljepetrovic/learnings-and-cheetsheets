# Entity Framework Learnings & Cheatsheet

This repository represents my personal learning and cheatsheet repository with examples and learnings that I have found so far.

This project contains list of the efficient Entity Framework Core good practices for performance improvement along with real-world use cases:

- Projection ✅
- Indexing ✅
- Usage of AsSplitQuery() that breaks down a query involving a large number of related entities into multiple SQL queries, reducing the size of the result set ✅
- Usage AsNoTracking() that improves performance for read-only operations ✅
- Usage of Async methods ✅
- Usage of SaveChangesAsync() once to reduce number of trips to the database. ✅