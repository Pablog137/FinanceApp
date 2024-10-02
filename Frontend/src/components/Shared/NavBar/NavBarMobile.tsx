import { useState } from "react";
import { useDarkMode } from "../../../context/DarkModeContext";
import LightModeIcon from "../../UI/Icons/LightModeIcon";
import NotificationIcon from "../../UI/Icons/NotificationIcon";

export default function NavBarMobile() {
  const [showProfile, setShowProfile] = useState(false);
  const { isDarkMode, toggleDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";

  return (
    <nav
      className={`${bgColor + " " + textColor} w-full dark:border-gray-700 p-1`}
    >
      <div className="px-3 py-3 lg:px-5 lg:pl-3">
        <div className="grid grid-cols-12">
          <div className="col-span-3 flex items-center gap-4">
            <button
              type="button"
              className="flex text-sm bg-gray-800 rounded-full focus:ring-4 focus:ring-gray-300 dark:focus:ring-gray-600"
              aria-expanded="false"
              data-dropdown-toggle="dropdown-user"
              onClick={() => setShowProfile(!showProfile)}
            >
              <span className="sr-only">Open user menu</span>
              <img
                className="w-8 h-8 rounded-full"
                src="https://media.istockphoto.com/id/1495088043/vector/user-profile-icon-avatar-or-person-icon-profile-picture-portrait-symbol-default-portrait.jpg?s=612x612&w=0&k=20&c=dhV2p1JwmloBTOaGAtaA3AW1KSnjsdMt7-U_3EZElZ0="
                alt="user photo"
              />
            </button>
            {showProfile && (
              <div
                className={`z-50 absolute mt-40 left-0 list-none bg-gray-100 divide-gray-100 rounded-lg shadow dark:bg-gray-700 dark:divide-gray-600`}
                id="user-dropdown"
              >
                <div className="px-4 py-3">
                  <span className="block text-sm text-gray-900 dark:text-white">
                    Pepe
                  </span>
                  <span className="block text-sm  text-gray-500 truncate dark:text-gray-400">
                    fermin
                  </span>
                </div>
                <ul className="list-none" aria-labelledby="user-menu-button">
                  <li>
                    <a
                      href="#"
                      className="block px-4 py-3 text-sm text-gray-700 hover:bg-gray-300 hover:rounded-lg hover:text-black hover:font-semibold dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white"
                      //   onClick={handleSignOut}
                    >
                      Sign out
                    </a>
                  </li>
                </ul>
              </div>
            )}

            <div
              className={`z-50 block my-4 text-base list-none bg-white divide-y divide-gray-100 rounded shadow dark:bg-gray-700 dark:divide-gray-600`}
              id="dropdown-user"
            ></div>
          </div>
          <div className="col-span-6 text-center text-2xl font-semibold">
            FinanceApp
          </div>
          <div className="col-span-3 flex justify-end gap-4">
            <NotificationIcon />
            <LightModeIcon
              isDarkMode={isDarkMode}
              toggleDarkMode={toggleDarkMode}
            />
          </div>
        </div>
      </div>
    </nav>
  );
}
