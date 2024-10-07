interface RecentTransferElementProps {
  name: string;
  amount: number;
}

export default function RecentTransferElement({
  name,
  amount,
}: RecentTransferElementProps) {
  const randomNumber = Math.floor(Math.random() * 100);
  return (
    <li>
      <div className="flex flex-col items-center gap-4 p-5 bg-gray-200 rounded-md hover:bg-gray-300">
        <img
          src={`https://randomuser.me/api/portraits/med/men/${randomNumber}.jpg`}
          className="rounded-full"
        />
        <div>
          <h1 className="text-md font-semibold">{name}</h1>
          <p className="text-sm text-gray-500">
            ${amount} <span className="text-xs font-semibold">USD</span>
          </p>
        </div>
      </div>
    </li>
  );
}
