import { useState } from "react";
import Aside from "../components/Dashboard/Aside";
import Main from "../components/Dashboard/Main";
import Navbar from "../components/Dashboard/NavBar";

export default function StructureContainer() {
  const [isAsideOpen, setIsAsideOpen] = useState(true);

  const toggleAside = () => {
    console.log("toggle");
    setIsAsideOpen(!isAsideOpen);
  };

  //    const colsAside = isAsideOpen ? "col-span-1 lg:col-span-2" : "hidden";
  const colMain = isAsideOpen
    ? "col-span-11 sm:col-span-11 lg:col-span-10"
    : "col-span-12";

  return (
    <>
      <Navbar isAsideOpen={isAsideOpen} toggleAside={toggleAside} />
      <div className="grid grid-cols-12">
        <div className="col-span-2">
          <Aside isAsideOpen={isAsideOpen} />
        </div>
        <Main colMain={colMain} />
      </div>
    </>
  );
}
