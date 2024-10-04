import { useDarkMode } from "../../context/DarkModeContext";
import BasicLineChart from "./BasicLineChart";
import Card from "./Card";
import QuickActions from "./QuickActions";
import SchedulePayments from "./SchedulePayments";

type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  const { textColor } = useDarkMode();
  const textColorCards = "text-gray-700";

  return (
    <>
      <div
        className={`${textColor} pt-5 md:pt-10 ${colMain} grid grid-cols-12 p-10 pb-20`}
      >
        <div className="text-white p-10 col-span-12 md:col-span-6 xl:col-span-4">
          <Card />
        </div>
        <div className="p-5 hidden md:col-span-6 xl:col-span-8 md:grid grid-cols-12 gap-4 items-center">
          <div className={`${textColorCards} col-span-12 xl:col-span-6`}>
            <div className="flex flex-col">
              <div className="bg-gray-100 border-2 p-6 rounded-md flex justify-between">
                <div className="flex flex-col gap-2">
                  <h1 className="font-semibold">Total expenses</h1>
                  <div className="flex justify-between gap-4">
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        Last month
                      </p>
                      <p className="text-2xl font-semibold">$ 1,000</p>
                    </div>
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        This month
                      </p>
                      <p className="text-2xl font-semibold">$ 56,000</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="col-span-12 xl:col-span-6">
            <div className={`${textColorCards} flex flex-col`}>
              <div className="bg-gray-100 border-2  p-6 rounded-md flex justify-between">
                <div className="flex flex-col gap-2">
                  <h1 className="font-semibold">Total incomes</h1>
                  <div className="flex justify-between gap-4">
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        Last month
                      </p>
                      <p className="text-2xl font-semibold">$ 1,000</p>
                    </div>
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        This month
                      </p>
                      <p className="text-2xl font-semibold">$ 56,000</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <BasicLineChart />
        <QuickActions />
        <SchedulePayments />
      </div>
    </>
  );
}
