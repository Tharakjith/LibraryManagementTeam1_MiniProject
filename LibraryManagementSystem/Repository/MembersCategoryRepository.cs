using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class MembersCategoryRepository : IMembersCategoryRepository
    {
        private readonly LibraryMngtDbContext _context;

        public MembersCategoryRepository(LibraryMngtDbContext context)
        {
            _context = context; //_context - virtual
        }

        #region   1 -  Get all members from DB - Search All
        public async Task<ActionResult<IEnumerable<Member>>> GetTblMembers()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Members.ToListAsync();
                }
                return new List<Member>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   2 - Get an Member based on Id
        public async Task<ActionResult<Member>> GetMembersById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var member = await _context.Members.FirstOrDefaultAsync(e => e.MemberId == id);
                    return member;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   3  - Insert an Member -return Member record
        public async Task<ActionResult<Member>> PostTblMemberReturnRecord(Member member)
        {
            try
            {
                if (member == null)
                {
                    throw new ArgumentException(nameof(member), "Member data is null");
                    
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.Members.AddAsync(member);

                await _context.SaveChangesAsync();

                var mem = await _context.Members 
                    .FirstOrDefaultAsync(e => e.MemberId == member.MemberId);

                return mem;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   4 - Insert an Member -return Id
        public async Task<ActionResult<int>> PostTblMemberReturnId(Member member)
        {
            try
            {
                if (member == null)
                {
                    throw new ArgumentException(nameof(member), "Member data is null");
                    
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.Members.AddAsync(member);

                var changesRecord = await _context.SaveChangesAsync();

                if (changesRecord > 0)
                {
                    return member.MemberId;
                }
                else
                {
                    throw new Exception("Failed to save Member record to the database");
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   5  - Update an Member with ID
        public async Task<ActionResult<Member>> PutTblMember(int id, Member member)
        {
            try
            {
                if (member == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                var existingMember = await _context.Members.FindAsync(id);
                if (existingMember == null)
                {
                    return null;
                }

                existingMember.MemberName = member.MemberName;
                existingMember.Email = member.Email;
                existingMember.PhoneNumber = member.PhoneNumber;

                await _context.SaveChangesAsync();

                var memrec = await _context.Members.FirstOrDefaultAsync(existingMember => existingMember.MemberId == member.MemberId);

                return memrec;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  6  - Delete an Member
        public JsonResult DeleteMemberById(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid Member Id"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                var existingMember = _context.Members.Find(id);

                if (existingMember == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Member not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                _context.Members.Remove(existingMember);

                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "Member Deleted successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion

        #region  7  - Get all Category list
        public async Task<ActionResult<IEnumerable<Category>>> GetTblCategories()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Categories.ToListAsync();
                }
                return new List<Category>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
