interface SchedulePaymentElementProps {
  title: string;
  nextPayment: string;
  amount: number;
  icon: string;
}

export default function SchedulePaymentElement({
  title,
  nextPayment,
  amount,
  icon,
}: SchedulePaymentElementProps) {
  return (
    <li className="p-4 rounded-md bg-white flex justify-between items-center">
      <i className={`${icon} text-2xl text-green-300`}></i>
      <div className="flex flex-col">
        <h5 className="text-xl font-semibold">{title}</h5>
        <p className="font-medium text-gray-400">
          Next Payment:{" "}
          <span className="text-blue-300 font-semibold">{nextPayment}</span>
        </p>
      </div>
      <p className="text-xl font-bold">
        ${amount}
        <span className="text-sm">USD</span>
      </p>
    </li>
  );
}
