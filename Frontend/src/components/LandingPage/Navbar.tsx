import { Link } from "react-router-dom";

export default function NavBar() {
  return (
    <nav className="border-gray-200 col-span-6 grid grid-cols-12 items-center justify-end gap-4 ">
      <ul className="text-white flex col-span-8 justify-end gap-12 text-lg me-5">
        <li>
          <a href="Pricing" className="hover:text-green-200">
            Pricing
          </a>
        </li>
        <li>
          <a href="Features" className="hover:text-green-200">
            Features
          </a>
        </li>
      </ul>
      <div className="col-span-4 flex justify-center gap-4">
        <Link to="/login">
          <button className="text-white px-4 py-2 rounded-sm border hover:border-green-300 hover:border-2">
            Sign in
          </button>
        </Link>
        <Link to="/register">
          <button className="text-white px-4 py-2 rounded-sm border hover:border-green-300 hover:border-2">
            Sign Up
          </button>
        </Link>
      </div>
    </nav>
  );
}
