export default function SearchContact() {
  return (
    <div className="px-10 md:px-20 xl:px-40">
      <div className="rounded-lg bg-gray-200 p-1">
        <form className="flex items-center gap-1 w-full text-md px-2">
          <i className="fa-solid fa-magnifying-glass text-gray-500"></i>
          <input
            type="text"
            name="search"
            placeholder="Search contact..."
            className="bg-gray-200 text-gray-700 placeholder-gray-500 px-1 py-1 w-full border-0 outline-none rounded-lg"
          />
        </form>
      </div>
    </div>
  );
}
