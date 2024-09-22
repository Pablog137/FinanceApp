import "../../styles/components/LandingPage/Hero.css";
import { Link } from "react-router-dom";

export default function Hero({ textColor }: { textColor: string }) {
  return (
    <>
      <div className={`grid grid-cols-12 justify-center mb-32 ${textColor}`}>
        <div className="col-span-12 mt-20 md:mt-40 flex justify-center items-center">
          <h1 className="text-3xl sm:text-4xl md:text-6xl xl:text-7xl  text-center font-bold">
            Know where your money goes{" "}
            <p className="gradient">to control your finances</p>
          </h1>
        </div>
        <div className="col-span-12 flex justify-center items-center mt-12 p-3">
          <h5 className="text-md sm:text-lg md:text-xl text-center font-medium">
            Spend less than your earn. Achieve financial dreams and experience a
            brighter financial future
          </h5>
        </div>
        <div className="col-span-12 flex justify-center items-center mt-12">
          <Link to="/register">
            <button className="bg-[#5897A3] hover:bg-[#3b98ab] font-semibold px-4 py-2 rounded-md text-white">
              Get started
            </button>
          </Link>
        </div>
      </div>
    </>
  );
}
