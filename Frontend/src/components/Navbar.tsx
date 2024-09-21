import { Link } from "react-router-dom";

export default function NavBar() {
  return (
    <nav className="border-gray-200 col-span-6 flex items-center justify-end">
      <Link to="/login">
        <button className="text-white hover:bg-gray-100 hover:text-black  px-4 py-2 rounded-md">
          Log in
        </button>
      </Link>
      <Link to="/register">
        <button className="text-white hover:bg-gray-100 hover:text-black px-4 py-2 rounded-md">
          Sign Up
        </button>
      </Link>
    </nav>
  );
}
