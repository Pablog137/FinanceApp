import { useState } from "react";
import Aside from "../components/Dashboard/Aside";
import Navbar from "../components/Dashboard/NavBar";
import NavBarMobile from "../components/Dashboard/NavBarMobile";

interface StructureContainerProps {
  MainComponent: any;
}

export default function StructureContainer({
  MainComponent,
}: StructureContainerProps) {
  const [isAsideOpen, setIsAsideOpen] = useState(true);

  const toggleAside = () => {
    setIsAsideOpen(!isAsideOpen);
  };

  const colsAside = isAsideOpen ? "col-span-1 lg:col-span-2" : "hidden";
  const colMain = isAsideOpen
    ? "col-span-11 sm:col-span-11 lg:col-span-10"
    : "col-span-12";

  return (
    <>
      <NavBarMobile isAsideOpen={isAsideOpen} toggleAside={toggleAside} />
      <div className="grid grid-cols-12">
        <div className={colsAside}>
          <Aside />
        </div>
        <MainComponent colMain={colMain} />
      </div>
    </>
  );
}
